﻿using Eto.Forms;
using Eto.Drawing;
using Eto.Platform.Wpf;
using Eto.Platform.Wpf.Forms;
using Eto.Platform.Wpf.Drawing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using swf = System.Windows.Forms;

namespace TestApplicationforWinformCalEto.cs
{
    public class MyDateTimePickerHandler : WindowsFormHostHandler<swf.DateTimePicker, DateTimePicker>, IDateTimePicker
    {

        swf.DateTimePicker picker;
        public MyDateTimePickerHandler() : base(new System.Windows.Forms.DateTimePicker())
        {
            picker = swfControl;
            picker.ShowCheckBox = false;
            Mode = DateTimePicker.DefaultMode;
            Value = null;
            picker.ValueChanged += delegate
            {
                Widget.OnValueChanged(EventArgs.Empty);
            };
            
        }

        public DateTimePickerMode Mode
        {
            get
            {
                switch (picker.Format)
                {
                    case System.Windows.Forms.DateTimePickerFormat.Long:
                        return DateTimePickerMode.DateTime;
                    case System.Windows.Forms.DateTimePickerFormat.Short:
                        return DateTimePickerMode.Date;
                    case System.Windows.Forms.DateTimePickerFormat.Time:
                        return DateTimePickerMode.Time;
                    default:
                        throw new NotImplementedException();
                }
            }
            set
            {
                switch (value)
                {
                    case DateTimePickerMode.DateTime:
                        picker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                        var format = CultureInfo.CurrentUICulture.DateTimeFormat;
                        picker.CustomFormat = format.ShortDatePattern + " " + format.LongTimePattern;
                        break;
                    case DateTimePickerMode.Date:
                        picker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
                        break;
                    case DateTimePickerMode.Time:
                        picker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public DateTime MinDate
        {
            get
            {
                return picker.MinDate;
            }
            set
            {
                picker.MinDate = value;
            }
        }

        public DateTime MaxDate
        {
            get
            {
                return picker.MaxDate;
            }
            set
            {
                picker.MaxDate = value;
            }
        }

        public DateTime? Value
        {
            get
            {
                if (!picker.Checked) return null;
                return picker.Value;
            }
            set
            {
                if (value != null)
                {
                    picker.Value = value.Value;
                    picker.Checked = true;
                }
                else
                    picker.Checked = false;
            }
        }
    }
}