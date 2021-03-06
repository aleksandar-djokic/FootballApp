using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.CustomValidation
{
    public class ValidateImage:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                return true;
            }

            if (file.ContentLength > 4 * 1024 * 1024)
            {
                return false;
            }

            try
            {
                using (var img = Image.FromStream(file.InputStream))
                {
                    return (img.RawFormat.Equals(ImageFormat.Png)|| img.RawFormat.Equals(ImageFormat.Jpeg));
                }
            }
            catch { }
            return false;
        }
    }
}