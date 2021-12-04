using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Net.Mime;
using System.Globalization;
/// <summary>
/// Summary description for Class1
/// </summary>

public class Class1
{
   
    public Class1()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    static string source = ConfigurationManager.ConnectionStrings["IntranetConnectionString"].ConnectionString;
    public SqlConnection getconnection()
    {
        
        SqlConnection con = new SqlConnection(source);
        con.Open();
        return con;
    }
    public string getConnectionString()
    {
        return source;
    }
    public string DateValforFilename()
    {
        string datval = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        return datval;
    }
    public bool IsValidEmail(string emailaddress)
    {
        bool flag = false;
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(emailaddress);
        if (match.Success)
            flag = true;
        else
            flag = false;
        return flag;
    }
    public  string TextCapatlized(string value)
    {
        char[] array = value.ToCharArray();
        // Handle the first letter in the string.
        if (array.Length >= 1)
        {
            if (char.IsLower(array[0]))
            {
                array[0] = char.ToUpper(array[0]);
            }
        }
        // Scan through the letters, checking for spaces.
        // ... Uppercase the lowercase letters following spaces.
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i - 1] == ' ')
            {
                if (char.IsLower(array[i]))
                {
                    array[i] = char.ToUpper(array[i]);
                }
            }
        }
        return new string(array);
    }
    public bool IsValidPhone(string phone)
    {
        bool flag = false;
        Regex regex = new Regex(@"^[0-9,+,-]{10,15}$");
        Match match = regex.Match(phone);
        if (match.Success)
            flag = true;
        else
            flag = false;
        return flag;
    }
    public bool IsValidPin(string pin)
    {
        bool flag = false;
        Regex regex = new Regex(@"^\d{6}");
        Match match = regex.Match(pin);
        if (match.Success)
            flag = true;
        else
            flag = false;
        return flag;
    }
    public bool IsValidEmpCode(string Code)
    {
        bool flag = false;
        Regex regex = new Regex(@"^\d{6}");
        Match match = regex.Match(Code);
        if (match.Success)
            flag = true;
        else
            flag = false;
        return flag;
    }
    public bool IsNumberOnly(string number)
    {
        string Str = number;
        double Num;
        bool isNum = double.TryParse(Str, out Num);
        return isNum;
    }
    public bool IsTextOnly(string text)
    {
        bool flag = false;
        Regex regex = new Regex(@"^[a-zA-Z]+$");
        Match match = regex.Match(text);
        if (match.Success)
            flag = true;
        else
            flag = false;
        return flag;
    }
    public bool IsAlphanumeric(string Alphanumeric)
    {
        bool flag = false;
        Regex regex = new Regex(@"^[0-9a-zA-Z]+$");
        Match match = regex.Match(Alphanumeric);
        if (match.Success)
            flag = true;
        else
            flag = false;
        return flag;

    }
    public bool IsValidDate(string Date)
    {
        bool flag = false;
        DateTime Test;
        if (DateTime.TryParseExact(Date, "dd/MM/yyyy", null, DateTimeStyles.None, out Test) == true)
            flag = true;
        else
            flag = false;
        return flag;
    }
    public bool IsValidAge(string Age)
    {
        bool flag = false;

        DateTime dob = Convert.ToDateTime(Age);
        DateTime Today = DateTime.Now;
        TimeSpan ts = Today - dob;
        DateTime Age1 = DateTime.MinValue + ts;


        // note: MinValue is 1/1/1 so we have to subtract...
        int Years = Age1.Year - 1;
        if (Years >= 18)
            flag = true;
        else
            flag = false;
        return flag;
    }
    public bool IsDateBeforeOrEqualToday(string input)
    {
        var parameterDate = DateTime.ParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        var todaysDate = DateTime.Today;

        if (parameterDate <= todaysDate)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool IsDateBeforeToday(string input)
    {
        var parameterDate = DateTime.ParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        var todaysDate = DateTime.Today;

        if (parameterDate < todaysDate)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool IsDateBeforeOrToday(string input)
    {
        var parameterDate = DateTime.ParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        var todaysDate = DateTime.Today;

        if (parameterDate < todaysDate)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool IsEndDateBeforeBeginDate(string begin, string last)
    {
        var begindate = DateTime.ParseExact(begin, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        var lastdate = DateTime.ParseExact(last, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        if (lastdate < begindate)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    public bool IsRunning(string begin, string last)
    {
        

            var begindate = DateTime.ParseExact(begin, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var lastdate = DateTime.ParseExact(last, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var c = DateTime.Now.ToString("dd/MM/yyyy");

            var curdate = DateTime.ParseExact(c, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (begindate <= curdate && lastdate >= curdate)
            {
                return true;
            }
            else
            {
                return false;
            }
       
        

    }
    public string fillCurrentDate()
    {
        DateTime newdt = DateTime.Now.AddHours(5.5);
        int d, m, y;
        //d = newdt.Day;
        //m = newdt.Month;
        //y = newdt.Year;
        d = DateTime.Now.Day;
        m = DateTime.Now.Month;
        y = DateTime.Now.Year;
        string dd, mm, dt;
        if (d < 10)
            dd = "0" + d.ToString();
        else
            dd = d.ToString();

        if (m < 10)
            mm = "0" + m.ToString();
        else
            mm = m.ToString();

        dt = dd + "/" + mm + "/" + y.ToString();
        return dt;
    }   
    public string makedate(string dt)
    {
        string d = dt.Substring(0, 2);

        int ms = 3;
        int ystart = 6;
        if (d.Substring(1, 1) == "/")
        {
            d = dt.Substring(0, 1);
            ystart -= 1;
            ms -= 1;
        }
        string m = dt.Substring(ms, 2);
        if (m.Substring(1, 1) == "/")
        {
            m = dt.Substring(ms, 1);
            ystart -= 1;
        }
        string y = dt.Substring(ystart, 4);

        // if website display in web server 
        string date1 = m + "/" + d + "/" + y;
        // if website display in local server 
        //string date1 = d + "/" + m + "/" + y;
        return date1;
    }
    public Int32 dateNumber(string dt)
    {
        Int32 dateno = 0;
        try
        {
            
            string d = dt.Substring(0, 2);

            int ms = 3;
            int ystart = 6;
            if (d.Substring(1, 1) == "/")
            {
                d = dt.Substring(0, 1);
                ystart -= 1;
                ms -= 1;
            }
            string m = dt.Substring(ms, 2);
            if (m.Substring(1, 1) == "/")
            {
                m = dt.Substring(ms, 1);
                ystart -= 1;
            }
            string y = dt.Substring(ystart, 4);

            // if website display in web server 
            string date1 = m + "/" + d + "/" + y;
            // if website display in local server 
            //string date1 = d + "/" + m + "/" + y;

            dateno = Convert.ToInt32(y) * 10000 + Convert.ToInt32(m) * 100 + Convert.ToInt32(d);
            
        }
        catch (Exception)
        {
        }
        return dateno;
    }
    public string monthname(int mmonth)
    {
        string str = "";
        switch (mmonth)
        {
            case 1: str = "Jan"; break;
            case 2: str = "Feb"; break;
            case 3: str = "Mar"; break;
            case 4: str = "Apr"; break;
            case 5: str = "May"; break;
            case 6: str = "Jun"; break;
            case 7: str = "Jul"; break;
            case 8: str = "Aug"; break;
            case 9: str = "Sep"; break;
            case 10: str = "Oct"; break;
            case 11: str = "Nov"; break;
            case 12: str = "Dec"; break;
        }
        return str;
    }
    public string monthnamefull(int mmonth)
    {
        string str = "";
        switch (mmonth)
        {
            case 1: str = "January"; break;
            case 2: str = "February"; break;
            case 3: str = "March"; break;
            case 4: str = "April"; break;
            case 5: str = "May"; break;
            case 6: str = "June"; break;
            case 7: str = "July"; break;
            case 8: str = "August"; break;
            case 9: str = "September"; break;
            case 10: str = "October"; break;
            case 11: str = "November"; break;
            case 12: str = "December"; break;
        }
        return str;
    }
    public string monthNo(string mmonth)
    {
        string str = "";
        switch (mmonth)
        {
            case "Jan": str = "1"; break;
            case "Feb": str = "2"; break;
            case "Mar": str = "3"; break;
            case "Apr": str = "4"; break;
            case "May": str = "5"; break;
            case "Jun": str = "6"; break;
            case "Jul": str = "7"; break;
            case "Aug": str = "8"; break;
            case "Sep": str = "9"; break;
            case "Oct": str = "10"; break;
            case "Nov": str = "11"; break;
            case "Dec": str = "12"; break;
        }
        return str;
    } 
    public string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }
    public string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
    public string GetEncryptActivationKey(string clearText,int KeyPair)
    {
        string EncryptionKey = "";
        if (KeyPair == 1)
            EncryptionKey = "MAKV2SPBNI99212";
        else if (KeyPair == 2)
            EncryptionKey = "NBLW3TQCOJ00323";
        else if (KeyPair == 3)
            EncryptionKey = "OCMX4URDPK11434";
        else if (KeyPair == 4)
            EncryptionKey = "PDNY5VSEQL22545";
        else if (KeyPair == 5)
            EncryptionKey = "QEOZ6WTFRM33656";
        else if (KeyPair == 6)
            EncryptionKey = "RFPA7XUGSN44767";
        else if (KeyPair == 7)
            EncryptionKey = "SGQB8YVHTO55878";


        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }
    public string GetDecryptActivationKey(string cipherText, int KeyPair)
    {
        string EncryptionKey = "";
        if (KeyPair == 1)
            EncryptionKey = "MAKV2SPBNI99212";
        else if (KeyPair == 2)
            EncryptionKey = "NBLW3TQCOJ00323";
        else if (KeyPair == 3)
            EncryptionKey = "OCMX4URDPK11434";
        else if (KeyPair == 4)
            EncryptionKey = "PDNY5VSEQL22545";
        else if (KeyPair == 5)
            EncryptionKey = "QEOZ6WTFRM33656";
        else if (KeyPair == 6)
            EncryptionKey = "RFPA7XUGSN44767";
        else if (KeyPair == 7)
            EncryptionKey = "SGQB8YVHTO55878";

        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }


    public string ForgotPushMail(string msg, string reqtype, string mailid)
    {
        try
        {
            string credential_emailid = "mailnoreply.acmp@gmail.com";
            string credential_password = "Aco007#123";

            MailMessage message = new MailMessage(credential_emailid, mailid, "ERHQ-1 - " + reqtype, msg);


            message.IsBodyHtml = true;
            //message.CC.Add(new MailAddress("kksoni@acompworld.com"));


            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential(credential_emailid, credential_password);
            client.Send(message);

            return "true";

        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
    }
}
