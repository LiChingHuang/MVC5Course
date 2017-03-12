using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.Validations
{
    public class 商品名稱不能有TestAttribute : DataTypeAttribute  //自訂驗證
    {
        public 商品名稱不能有TestAttribute() : base(DataType.Text)
        {
            this.ErrorMessage = "不能有Test的字樣!!"; //覆寫錯誤訊息
        }

        public override bool IsValid(object value)
        {
            string str = Convert.ToString(value);
            if (str.Contains("Test"))
            {
                return false;
            }
            return true;
            //return base.IsValid(value);
        }
    }
}