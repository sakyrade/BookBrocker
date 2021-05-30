using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SearchBooksApp
{
    //Класс для реализации функции шифрования пароля
    public class Crypto
    {
        //Метод, шифрующий пароль с помощью алгоритма криптографического хэширования SHA-1
        public static string CreateHashCode(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                byte[] tmpHash = new SHA1CryptoServiceProvider().ComputeHash(ASCIIEncoding.ASCII.GetBytes(str));
                StringBuilder sOutput = new StringBuilder(tmpHash.Length);
                for (int i = 0; i < tmpHash.Length; i++)
                    sOutput.Append(tmpHash[i].ToString("X2"));
                return sOutput.ToString();
            }
            return null;
        }
    }
}
