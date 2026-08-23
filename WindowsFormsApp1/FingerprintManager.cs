using libzkfpcsharp;
using System;
using System.Threading;

namespace WindowsFormsApp1
{
    public class FingerprintCapture
    {
        public byte[] Image { get; set; }
        public byte[] Template { get; set; }
        public int TemplateSize { get; set; }
    }


    public sealed class FingerprintManager
    {
        // =========================================================
        // SINGLETON
        // =========================================================

        private static readonly FingerprintManager instance =
            new FingerprintManager();

        public static FingerprintManager Instance
        {
            get { return instance; }
        }


        // =========================================================
        // SDK
        // =========================================================

        private IntPtr mDevHandle = IntPtr.Zero;
        private IntPtr mDBHandle = IntPtr.Zero;

        private Thread captureThread = null;

        private volatile bool stopCapture = false;

        private bool initialized = false;


        // =========================================================
        // IMAGE INFORMATION
        // =========================================================

        private int mfpWidth = 0;
        private int mfpHeight = 0;

        private byte[] FPBuffer;


        // =========================================================
        // CURRENT CAPTURE CALLBACK
        // =========================================================

        private Action<FingerprintCapture> captureCallback;


        // =========================================================
        // PROPERTIES
        // =========================================================

        public bool IsInitialized
        {
            get { return initialized; }
        }

        public bool IsConnected
        {
            get
            {
                return initialized &&
                       mDevHandle != IntPtr.Zero;
            }
        }

        public IntPtr DBHandle
        {
            get { return mDBHandle; }
        }

        public int ImageWidth
        {
            get { return mfpWidth; }
        }

        public int ImageHeight
        {
            get { return mfpHeight; }
        }


        // =========================================================
        // CONSTRUCTOR
        // =========================================================

        private FingerprintManager()
        {
        }


        // =========================================================
        // INITIALIZE SDK
        // =========================================================

        public bool Initialize(out string error)
        {
            error = null;

            if (initialized)
                return true;


            int ret = zkfp2.Init();

            if (ret != zkfperrdef.ZKFP_ERR_OK)
            {
                error =
                    "Fingerprint SDK initialization failed. Error: "
                    + ret;

                return false;
            }


            // -----------------------------------------------------
            // Check scanner
            // -----------------------------------------------------

            int deviceCount =
                zkfp2.GetDeviceCount();


            if (deviceCount <= 0)
            {
                zkfp2.Terminate();

                error =
                    "Fingerprint scanner not connected.";

                return false;
            }


            // -----------------------------------------------------
            // Open first scanner
            // -----------------------------------------------------

            mDevHandle =
                zkfp2.OpenDevice(0);


            if (mDevHandle == IntPtr.Zero)
            {
                zkfp2.Terminate();

                error =
                    "Failed to open fingerprint scanner.";

                return false;
            }


            // -----------------------------------------------------
            // Initialize fingerprint algorithm database
            // -----------------------------------------------------

            mDBHandle =
                zkfp2.DBInit();


            if (mDBHandle == IntPtr.Zero)
            {
                zkfp2.CloseDevice(mDevHandle);

                mDevHandle = IntPtr.Zero;

                zkfp2.Terminate();

                error =
                    "Failed to initialize fingerprint database.";

                return false;
            }


            // -----------------------------------------------------
            // Get image width
            // -----------------------------------------------------

            byte[] paramValue =
                new byte[4];

            int size = 4;


            ret =
                zkfp2.GetParameters(
                    mDevHandle,
                    1,
                    paramValue,
                    ref size
                );


            if (ret == zkfperrdef.ZKFP_ERR_OK)
            {
                zkfp2.ByteArray2Int(
                    paramValue,
                    ref mfpWidth
                );
            }


            // -----------------------------------------------------
            // Get image height
            // -----------------------------------------------------

            size = 4;


            ret =
                zkfp2.GetParameters(
                    mDevHandle,
                    2,
                    paramValue,
                    ref size
                );


            if (ret == zkfperrdef.ZKFP_ERR_OK)
            {
                zkfp2.ByteArray2Int(
                    paramValue,
                    ref mfpHeight
                );
            }


            if (mfpWidth <= 0 ||
                mfpHeight <= 0)
            {
                Shutdown();

                error =
                    "Invalid fingerprint scanner image dimensions.";

                return false;
            }


            FPBuffer =
                new byte[
                    mfpWidth *
                    mfpHeight
                ];


            initialized = true;


            return true;
        }


        // =========================================================
        // START CAPTURE
        // =========================================================

        public void StartCapture(
            Action<FingerprintCapture> callback)
        {
            if (!initialized)
                return;


            // Replace whoever currently receives captures.
            captureCallback = callback;


            // Already running.
            if (captureThread != null &&
                captureThread.IsAlive)
            {
                return;
            }


            stopCapture = false;


            captureThread =
                new Thread(
                    CaptureLoop
                );


            captureThread.IsBackground = true;

            captureThread.Start();
        }


        // =========================================================
        // STOP CAPTURE
        // =========================================================

        public void StopCapture()
        {
            captureCallback = null;

            stopCapture = true;


            if (captureThread != null &&
                captureThread.IsAlive)
            {
                captureThread.Join(1000);
            }


            captureThread = null;
        }


        // =========================================================
        // CAPTURE LOOP
        // =========================================================

        private void CaptureLoop()
        {
            byte[] capTmp =
                new byte[2048];


            while (!stopCapture)
            {
                if (mDevHandle == IntPtr.Zero)
                {
                    Thread.Sleep(200);
                    continue;
                }


                int cbCapTmp = 2048;


                int ret =
                    zkfp2.AcquireFingerprint(
                        mDevHandle,
                        FPBuffer,
                        capTmp,
                        ref cbCapTmp
                    );


                if (ret ==
                    zkfperrdef.ZKFP_ERR_OK)
                {
                    // -------------------------------------------------
                    // IMPORTANT:
                    //
                    // Make copies because capTmp and FPBuffer are
                    // reused on the next capture.
                    // -------------------------------------------------

                    byte[] imageCopy =
                        new byte[FPBuffer.Length];

                    Array.Copy(
                        FPBuffer,
                        imageCopy,
                        FPBuffer.Length
                    );


                    byte[] templateCopy =
                        new byte[cbCapTmp];

                    Array.Copy(
                        capTmp,
                        templateCopy,
                        cbCapTmp
                    );


                    FingerprintCapture capture =
                        new FingerprintCapture
                        {
                            Image = imageCopy,
                            Template = templateCopy,
                            TemplateSize = cbCapTmp
                        };


                    Action<FingerprintCapture> callback =
                        captureCallback;


                    if (callback != null)
                    {
                        try
                        {
                            callback(capture);
                        }
                        catch
                        {
                            // The form may have disappeared.
                        }
                    }
                }


                Thread.Sleep(200);
            }
        }


        // =========================================================
        // LOAD DATABASE FINGERPRINTS
        // =========================================================

        public int AddFingerprint(
            int employeeId,
            byte[] template)
        {
            if (mDBHandle == IntPtr.Zero)
                return zkfperrdef.ZKFP_ERR_NOT_INIT;


            return zkfp2.DBAdd(
                mDBHandle,
                employeeId,
                template
            );
        }


        // =========================================================
        // IDENTIFY
        // =========================================================

        public int Identify(
            byte[] template,
            ref int employeeId,
            ref int score)
        {
            if (mDBHandle == IntPtr.Zero)
                return zkfperrdef.ZKFP_ERR_NOT_INIT;


            return zkfp2.DBIdentify(
                mDBHandle,
                template,
                ref employeeId,
                ref score
            );
        }


        // =========================================================
        // MATCH
        // =========================================================

        public int Match(
            byte[] template1,
            byte[] template2)
        {
            if (mDBHandle == IntPtr.Zero)
                return zkfperrdef.ZKFP_ERR_NOT_INIT;


            return zkfp2.DBMatch(
                mDBHandle,
                template1,
                template2
            );
        }


        // =========================================================
        // MERGE THREE CAPTURES
        // =========================================================

        public int Merge(
            byte[] temp1,
            byte[] temp2,
            byte[] temp3,
            byte[] registeredTemplate,
            ref int registeredTemplateLength)
        {
            if (mDBHandle == IntPtr.Zero)
                return zkfperrdef.ZKFP_ERR_NOT_INIT;


            return zkfp2.DBMerge(
                mDBHandle,
                temp1,
                temp2,
                temp3,
                registeredTemplate,
                ref registeredTemplateLength
            );
        }


        // =========================================================
        // SHUTDOWN EVERYTHING
        // =========================================================

        public void Shutdown()
        {
            StopCapture();


            if (mDBHandle != IntPtr.Zero)
            {
                zkfp2.DBFree(
                    mDBHandle
                );

                mDBHandle =
                    IntPtr.Zero;
            }


            if (mDevHandle != IntPtr.Zero)
            {
                zkfp2.CloseDevice(
                    mDevHandle
                );

                mDevHandle =
                    IntPtr.Zero;
            }


            if (initialized)
            {
                zkfp2.Terminate();
            }


            initialized = false;
        }
    }
}