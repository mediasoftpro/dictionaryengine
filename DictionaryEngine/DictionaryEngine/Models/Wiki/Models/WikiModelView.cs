using Jugnoon.Framework;
using Jugnoon.Utility;

namespace Jugnoon.Models
{
    public class WikiModelView
    {
        public JGN_Wiki Data { get; set; }

        public string Message { get; set; }

        public AlertTypes AlertType { get; set; }

        public bool isAllowed { get; set; }

        public string DetailMessage { get; set; }
    }
}

/*
 * This file is subject to the terms and conditions defined in
 * file 'LICENSE.md', which is part of this source code package.
 * Copyright 2007 - 2020 MediaSoftPro
 * For more information email at support@mediasoftpro.com
 */
