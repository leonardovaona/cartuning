using System;
using System.Text;
using BE;
using System.Security.Cryptography;

public class Encrypter
{
    private static TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
    private static MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();

    private static byte[] MD5Hash(string value)
    {
        return MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(value));
    }

    private string _hiddenKey = "Hid33nK3y.M4x,$e(u&1t?=)";
    public static string EncriptarMD5(string stringToEncrypt, string key)
    {
        DES.Key = Encrypter.MD5Hash(key);
        DES.Mode = CipherMode.ECB;
        byte[] Buffer = ASCIIEncoding.ASCII.GetBytes(stringToEncrypt);
        return Convert.ToBase64String(DES.CreateEncryptor().TransformFinalBlock(Buffer, 0, Buffer.Length));
    }

    public static string DesencriptarMD5(string encryptedString, string key)
    {
        try
        {
            DES.Key = Encrypter.MD5Hash(key);
            DES.Mode = CipherMode.ECB;
            byte[] Buffer = Convert.FromBase64String(encryptedString);
            return ASCIIEncoding.ASCII.GetString(DES.CreateDecryptor().TransformFinalBlock(Buffer, 0, Buffer.Length));
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public static string EncriptarSHA512(string stringToHash)
    {
        SHA512CryptoServiceProvider sha = new SHA512CryptoServiceProvider();
        byte[] bytesToHash = System.Text.Encoding.ASCII.GetBytes(stringToHash);
        bytesToHash = sha.ComputeHash(bytesToHash);
        string strResult = "";
        foreach (byte b in bytesToHash)
            strResult += b.ToString("x2");
        return strResult;
    }

    private static string ByteArrayToString(byte[] arrInput)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder(arrInput.Length * 2);
        for (int i = 0; i <= arrInput.Length - 1; i++)
            sb.Append(arrInput[i].ToString("X2"));
        return sb.ToString().ToLower();
    }
}
