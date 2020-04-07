using Jugnoon.Entity;
using Jugnoon.Framework;
using System.Collections.Generic;

namespace Jugnoon.Models
{
    public class GlossaryListView : ListViewModel
    {
        public string Character { get; set; }
        public int TotalRecords { get; set; }
        public List<JGN_Wiki> DataList { get; set; }
        public WikiEntity QueryOptions { get; set; }
    }
}

/*
 * This file is subject to the terms and conditions defined in
 * file 'LICENSE.md', which is part of this source code package.
 * Copyright 2007 - 2020 MediaSoftPro
 * For more information email at support@mediasoftpro.com
 */
