//--------------------------------------------
// Copyright (C) 北京日升天信科技股份有限公司
// filename :YewuKdhm
// created by 陈星宇
// at 2015/09/18 13:33:29
//--------------------------------------------
using System;

namespace MultiConsole
{
    public class YewuKdhm
    {
        private double _yewubl_bh;

        private double _kuandai_hm;

        private DateTime _inputtime;

        public DateTime Inputtime
        {
            get { return _inputtime; }
            set { _inputtime = value; }
        }

        public double Kuandai_hm
        {
            get { return _kuandai_hm; }
            set { _kuandai_hm = value; }
        }

        public double Yewubl_bh
        {
            get { return _yewubl_bh; }
            set { _yewubl_bh = value; }
        }
    }
}