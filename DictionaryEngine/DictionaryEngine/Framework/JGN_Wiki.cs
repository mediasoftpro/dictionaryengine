using System;
using System.ComponentModel.DataAnnotations;

namespace Jugnoon.Framework
{  
    public partial class JGN_Wiki
    {
        [Key]
        public int id { get; set; }
        [MaxLength(150)]
        public string term { get; set; }
        public string description { get; set; }
        [MaxLength(200)]
        public string term_complete { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
        public int replyid { get; set; }
        public int recommended { get; set; }
        [MaxLength(100)]
        public string userid { get; set; }
        [MaxLength(300)]
        public string tags { get; set; }
    }
}

/*
 * This file is subject to the terms and conditions defined in
 * file 'LICENSE.md', which is part of this source code package.
 * Copyright 2007 - 2020 MediaSoftPro
 * For more information email at support@mediasoftpro.com
 */

