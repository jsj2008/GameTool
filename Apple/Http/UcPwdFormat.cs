using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PublicUtilities;

namespace Apple.Http
{
    public partial class UcPwdFormat : UserControl
    {
        public UcPwdFormat()
        {
            InitializeComponent();
            this.Load += new EventHandler(UcPwdFormat_Load);
        }

        IList<CheckBox> chkList = new List<CheckBox>();
        void UcPwdFormat_Load(object sender, EventArgs e)
        {
            // chkList.Add(chkAll);
            chkList.Add(chkFirstCharReversal);
            chkList.Add(chkAddOneAfterEndWithNum);
            chkList.Add(chkAddAAfterEndWithChar);
            chkList.Add(chkAddAAllNum);
            chkList.Add(chkAddOneAllChar);
            chkList.Add(chkRawAddFirstCharReversal);
        }

        public IList<PwdFormatOptions> PwdOptions
        {
            get
            {
                return GetOptions();
            }
        }

        private IList<PwdFormatOptions> GetOptions()
        {
            IList<PwdFormatOptions> optionList = new List<PwdFormatOptions>();
            if (chkRaw.Checked)
            {
                return optionList;
            }
            else if (chkAll.Checked)
            {
                optionList.Add(PwdFormatOptions.FirstCharReversal);
                optionList.Add(PwdFormatOptions.AddOneAfterEndWithNum);
                optionList.Add(PwdFormatOptions.AddAAfterEndWithChar);
                optionList.Add(PwdFormatOptions.AddAAllNum);
                optionList.Add(PwdFormatOptions.AddOneAllChar);
                optionList.Add(PwdFormatOptions.RawAddFirstCharReversal);
                return optionList;
            }
            else if (chkFirstCharReversal.Checked)
            {
                optionList.Add(PwdFormatOptions.FirstCharReversal);
            }
            else if (chkAddOneAfterEndWithNum.Checked)
            {
                optionList.Add(PwdFormatOptions.AddOneAfterEndWithNum);
            }
            else if (chkAddAAfterEndWithChar.Checked)
            {
                optionList.Add(PwdFormatOptions.AddAAfterEndWithChar);
            }
            else if (chkAddAAllNum.Checked)
            {
                optionList.Add(PwdFormatOptions.AddAAllNum);
            }
            else if (chkAddOneAllChar.Checked)
            {
                optionList.Add(PwdFormatOptions.AddOneAllChar);
            }
            else if (chkRawAddFirstCharReversal.Checked)
            {
                optionList.Add(PwdFormatOptions.RawAddFirstCharReversal);
            }
            return optionList;
        }

        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkBox = sender as CheckBox;
            if ((chkBox == chkRaw) && chkRaw.Checked)
            {
                chkAll.Checked = false;
                SetChecked(false);
            }
            else if ((chkBox == chkAll) && chkAll.Checked)
            {
                chkRaw.Checked = false;
                SetChecked(true);
            }
            else if (chkList.Contains(chkBox) && !chkBox.Checked)
            {
                chkRaw.Checked = false;
                chkAll.Checked = false;
            }
        }

        private void SetChecked(bool isChecked)
        {
            foreach (CheckBox chk in chkList)
            {
                chk.Checked = isChecked;
            }
        }
    }
}
