//--------------------------------------------
// Copyright (C) 北京日升天信科技股份有限公司
// filename :DictionaryData
// created by 陈星宇
// at 2015/09/18 11:19:28
//--------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiConsole
{
    /// <summary>
    /// 数据字典表
    /// </summary>
    public class DictionaryData
    {
        /// <summary>
        /// 数据字典主键
        /// </summary>
        private string _dict_code;

        /// <summary>
        /// 父节点代号，根目录为0
        /// </summary>
        private string _dict_parent;

        /// <summary>
        /// 字典名称
        /// </summary>
        private string _dict_name;

        /// <summary>
        /// 字典值
        /// </summary>
        private string _dict_value;

        /// <summary>
        /// 字典状态
        /// 0 可用，1 停用。默认 0
        /// </summary>
        private string _dict_status;

        /// <summary>
        /// 字典固定
        /// 0 非固定，1 固定。默认 0
        /// </summary>
        private string _dict_isfixed;

        /// <summary>
        /// 字典描述
        /// </summary>
        private string _dict_remarks;

        /// <summary>
        /// 排序序号
        /// </summary>
        private double _dict_order;

        /// <summary>
        /// 最后编辑时间
        /// </summary>
        private double _last_edit_datetime;

        /// <summary>
        /// 创建时间
        /// </summary>
        private double _create_datetime;

        /// <summary>
        /// 创建时间
        /// </summary>
        public double create_datetime
        {
            get { return _create_datetime; }
            set { _create_datetime = value; }
        }

        /// <summary>
        /// 最后编辑时间
        /// </summary>
        public double last_edit_datetime
        {
            get { return _last_edit_datetime; }
            set { _last_edit_datetime = value; }
        }

        /// <summary>
        /// 排序序号
        /// </summary>
        public double dict_order
        {
            get { return _dict_order; }
            set { _dict_order = value; }
        }

        /// <summary>
        /// 字典描述
        /// </summary>
        public string dict_remarks
        {
            get { return _dict_remarks; }
            set { _dict_remarks = value; }
        }

        /// <summary>
        /// 字典固定
        /// 0 非固定，1 固定。默认 0
        /// </summary>
        public string dict_isfixed
        {
            get { return _dict_isfixed; }
            set { _dict_isfixed = value; }
        }

        /// <summary>
        /// 字典状态
        /// 0 可用，1 停用。默认 0
        /// </summary>
        public string dict_status
        {
            get { return _dict_status; }
            set { _dict_status = value; }
        }

        /// <summary>
        /// 字典值
        /// </summary>
        public string dict_value
        {
            get { return _dict_value; }
            set { _dict_value = value; }
        }

        /// <summary>
        /// 字典名称
        /// </summary>
        public string dict_name
        {
            get { return _dict_name; }
            set { _dict_name = value; }
        }

        /// <summary>
        /// 数据字典主键
        /// </summary>
        public string dict_code
        {
            get
            {
                return _dict_code;
            }
            set { _dict_code = value; }
        }

        /// <summary>
        /// 父节点代号，根目录为0
        /// </summary>
        public string dict_parent
        {
            get { return _dict_parent; }
            set { _dict_parent = value; }
        }
    }
}
