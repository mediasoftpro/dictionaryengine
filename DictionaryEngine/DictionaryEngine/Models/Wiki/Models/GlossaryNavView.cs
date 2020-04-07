namespace Jugnoon.Models
{
    public class GlossaryNavView
    {
        private string _character = "";
        private bool _isheader = false; // show heading text

        public string Character
        {
            get { return _character; }
            set { _character = value; }
        }

        public bool isHeader
        {
            get { return _isheader; }
            set { _isheader = value; }
        }
    }
}

/*
 * This file is subject to the terms and conditions defined in
 * file 'LICENSE.md', which is part of this source code package.
 * Copyright 2007 - 2020 MediaSoftPro
 * For more information email at support@mediasoftpro.com
 */
