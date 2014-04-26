using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WinAuthentication.Resources;

using SBSSLClient;
using SBSimpleSSL;
using SBZCommonUnit;
using SBSSLCommon;
using SBSSLConstants;
using SBHTTPSServer;
using SBUtils;
using SBCertValidator;
using SBConstants;
using SBX509;
using SBTypes;
using SBHTTPCRL;
using SBHTTPOCSPClient;
using SBHTTPCertRetriever;


namespace WinAuthentication
{
    public partial class MainPage : PhoneApplicationPage
    {
        private static string filename = "key.txt";

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            SBUtils.Unit.SetLicenseKey("2BABB64AA75B681BA6796909BB6F5DC371E136976DDC4BEBEAFCDA43DF136756A1FC337BBFFBB87C6C7CE4FCD8A33CF5599AC79FE4EA0F851B6EBAF191ED3D08871F57AE0C1F7201D850698E8B3472AAA929E89537BD8B0208D8F9937282D7E73C761289B8EEFD22C57AB3ADD2E459C78B8CFF64F7ED02DF560828F7C27DA4E888B947EE18D8D7C4C1962FAD06F04CA30897CA654FCEC8A8F43326C6DD4B163BC3CEAE448F47FF54D111E6CBA0B033FE5B6444AA77FC1C6E0AAC64E81CD374A04B330C7E4A032C3B737682827E72E177F1A899E7731A9A5F9866B01954E0947AAB8218406F300C856A20CCA3F8BE1B9FE34F5A134B61EB1D1DC3C8C3449F95D7");

            // TestWebService();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*
            string url = "https://192.168.159.140";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.BeginGetResponse(GetSampleCallback, request);
             * */
            TestCert();
        }

        public void TestCert()
        {

            var c = new TElSimpleSSLClient();
            c.OnCertificateValidate += new TSBCertificateValidateEvent(OnCertificateValidate);

            // Make sure the address is correct!
            c.Address = "192.168.159.149";
            c.Port = 443;

            try
            {
                c.Open();
            }
            catch
            {
                Console.WriteLine("socket connection failed");
            }
           
            c.Close(false);
        }

        private void OnCertificateValidate(object sender, TElX509Certificate x509certificate, ref TSBBoolean validate)
        {
            byte[] certPublicKey = new byte[1024];
            x509certificate.GetPublicKeyBlob(out certPublicKey);

            // Load previous key string
            byte[] prevCertKey;
            if (loadBlob(filename, out prevCertKey))
            {
                bool compResult = certPublicKey.SequenceEqual(prevCertKey);
                validate = compResult;
                tbWebServiceResult.Text = "Your certificate was " + (compResult ? "good" : "bad");
            }
            else
            {
                // Save on first time use
                saveBlob(filename, certPublicKey);

                validate = true;
                tbWebServiceResult.Text = "Saved certificate";
            }


        }

        private void saveBlob(string filename, byte[] blob)
        {

            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream rawStream = isf.CreateFile(filename))
                {
                    Console.WriteLine("Writing to file...");
                    rawStream.Write(blob, 0, blob.Length);
                    rawStream.Close();
                }


            }
        }


        private bool loadBlob(string filename, out byte[] result)
        {
            result = null;

            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isf.FileExists(filename))
                {
                    try
                    {
                        using (IsolatedStorageFileStream rawStream = isf.OpenFile(filename, System.IO.FileMode.Open))
                        {
                            int len = (int)rawStream.Length;
                            result = new byte[len];

                            rawStream.Read(result, 0, len);
                            rawStream.Close();
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }


}
