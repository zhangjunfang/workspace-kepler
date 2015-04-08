using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Utility.Security
{
    public class RSA
    {
        //对数据签名
        //public static string SignData(string str_DataToSign, string str_Private_Key)
        //{
        //    byte[] DataToSign = System.Text.Encoding.ASCII.GetBytes(str_DataToSign);
        //    try
        //    {
        //        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        //        RSA.ImportCspBlob(Convert.FromBase64String(str_Private_Key));
        //        byte[] signedData = RSA.SignData(DataToSign, new MD5CryptoServiceProvider());
        //        string str_SignedData = Convert.ToBase64String(signedData);
        //        return str_SignedData;
        //    }
        //    catch (CryptographicException e)
        //    {
        //        return e.Message;
        //    }
        //}

        /// <summary>
        /// 对数据签名
        /// </summary>
        /// <param name="str_DataToSign">要进行签名的数据</param>
        /// <returns>签名后的数据</returns>
        public static string SignData(string str_DataToSign)
        {
            X509Certificate2 x509_2 = new X509Certificate2(@"D:\wwwroot\HYH\Cert\yxcp.pfx", "123456");
            byte[] DataToSign = System.Text.Encoding.UTF8.GetBytes(str_DataToSign);
            try
            {
                RSACryptoServiceProvider RSA = (RSACryptoServiceProvider)x509_2.PrivateKey;
                return Convert.ToBase64String(RSA.SignData(DataToSign, new MD5CryptoServiceProvider()));
            }
            catch (CryptographicException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="str_DataToVerify">签名前数据</param>
        /// <param name="str_SignedData">要验证的签名数据</param>
        /// <returns>验证结果</returns>
        public static bool VerifyData(string str_DataToVerify, string str_SignedData,int fileType)
        {
            string[] arrFile = { @"D:\wwwroot\HYH\Cert\88_public.public", @"D:\wwwroot\HYH\Cert\ca-cert.cer" };
            X509Certificate2 x509_2 = new X509Certificate2(arrFile[fileType]);
            byte[] SignedData = Convert.FromBase64String(str_SignedData);
            byte[] DataToVerify = System.Text.Encoding.UTF8.GetBytes(str_DataToVerify);
            try
            {
                RSACryptoServiceProvider RSA = (RSACryptoServiceProvider)x509_2.PublicKey.Key;
                return RSA.VerifyData(DataToVerify, new MD5CryptoServiceProvider(), SignedData);
            }
            catch (CryptographicException e)
            {
                throw e;
            }
        }

        #region tz
        /// <summary>
        /// 对数据签名
        /// </summary>
        /// <param name="str_DataToSign">要进行签名的数据</param>
        /// <returns>签名后的数据</returns>
        //public static string SignDataTz(string str_DataToSign)
        //{
        //    X509Certificate2 x509_2 = new X509Certificate2(@"D:\wwwroot\HYH\Cert\yxcp.pfx", "123456");
        //    byte[] DataToSign = System.Text.Encoding.UTF8.GetBytes(str_DataToSign);
        //    try
        //    {
        //        RSACryptoServiceProvider RSA = (RSACryptoServiceProvider)x509_2.PrivateKey;
        //        return Convert.ToBase64String(RSA.SignData(DataToSign, new MD5CryptoServiceProvider()));
        //    }
        //    catch (CryptographicException e)
        //    {
        //        throw e;
        //    }
        //}

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="str_DataToVerify">签名前数据</param>
        /// <param name="str_SignedData">要验证的签名数据</param>
        /// <returns>验证结果</returns>
        //public static bool VerifyDataTz(string str_DataToVerify, string str_SignedData, int fileType)
        //{
        //    string[] arrFile = { @"D:\wwwroot\HYH\Cert\88_public.public", @"D:\wwwroot\HYH\Cert\yxcp.cer" };
        //    X509Certificate2 x509_2 = new X509Certificate2(arrFile[fileType]);
        //    byte[] SignedData = Convert.FromBase64String(str_SignedData);
        //    byte[] DataToVerify = System.Text.Encoding.UTF8.GetBytes(str_DataToVerify);
        //    try
        //    {
        //        RSACryptoServiceProvider RSA = (RSACryptoServiceProvider)x509_2.PublicKey.Key;
        //        return RSA.VerifyData(DataToVerify, new MD5CryptoServiceProvider(), SignedData);
        //    }
        //    catch (CryptographicException e)
        //    {
        //        throw e;
        //    }
        //}
        #endregion

        //对数据签名
        //public static string CreateSignature(string str_DataToSign, string str_Private_Key)
        //{
        //    byte[] DataToSign = System.Text.Encoding.ASCII.GetBytes(str_DataToSign);
        //    try
        //    {
        //        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        //        RSA.FromXmlString(str_Private_Key);
        //        RSAPKCS1SignatureFormatter RSAFormatter = new RSAPKCS1SignatureFormatter(RSA);
        //        RSAFormatter.SetHashAlgorithm("MD5");
        //        byte[] signedData = RSAFormatter.CreateSignature(DataToSign);
        //        string str_SignedData = Convert.ToBase64String(signedData);
        //        return str_SignedData;
        //    }
        //    catch (CryptographicException e)
        //    {
        //        return e.Message;
        //    }
        //}

        //验证签名
        //public static bool VerifySignature(string str_DataToVerify, string str_SignedData, string str_Public_Key)
        //{
        //    byte[] SignedData = Convert.FromBase64String(str_SignedData);
        //    byte[] DataToVerify = System.Text.Encoding.ASCII.GetBytes(str_DataToVerify);
        //    try
        //    {
        //        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        //        RSA.FromXmlString(str_Public_Key);
        //        RSAPKCS1SignatureDeformatter RSADeformatter = new RSAPKCS1SignatureDeformatter(RSA);
        //        RSADeformatter.SetHashAlgorithm("MD5");
        //        return RSADeformatter.VerifySignature(DataToVerify, SignedData);
        //    }
        //    catch (CryptographicException e)
        //    {
        //        //Console.WriteLine(e.Message);

        //        return false;
        //    }
        //}

    }
}
