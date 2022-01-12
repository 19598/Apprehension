using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.UI;

public class EmailCrashReport : MonoBehaviour
{
    public Text newText;

    public void updateText(string input)
    {
        newText.text = input;
    }

    public void checkText()
    {
        Debug.Log(newText.text);
    }

    public void clearText()
    {
        newText.text = "";
    }

    public void sendCrash()
    {
        SendEmailReport(newText.text);
    }

    /// <summary>
    /// Emails the user a crash report
    /// </summary>
    /// <param name="address">The address to send the email to</param>
    public static void SendEmailReport(string address)
    {
        try
        {
            //Creates the crash report string from available reports
            string reports = "Here is the crash report you requested:\n";
            foreach (CrashReport report in CrashReport.reports)
            {
                reports = reports + "\n" + report.text + "\n";
            }

            //Sets up the email
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("goodgaminggroupteam@gmail.com", "G.G.G.Support1"),
                EnableSsl = true,
            };

            //Sends the email
            smtpClient.Send("goodgaminggroupteam@gmail.com", "brendeg39@gmail.com", "Crash Report", reports);
        }
        catch { }
    }
}
