using Jugnoon.Framework;

namespace Jugnoon.Utility
{
    public class WikiUrlConfig
    {       
        public static string PrepareUrl(JGN_Wiki entity)
        {
            string _title = "";
            if (entity.term_complete == null && entity.term_complete == "")
                _title = entity.term;
            else
                _title = entity.term_complete;
            int maxium_length = Settings.Configs.GeneralSettings.maximum_dynamic_link_length;
            if (_title.Length > maxium_length && maxium_length > 0)
                _title = _title.Substring(0, maxium_length);
            else if (_title.Length < 3)
                _title = "preview-post";
            
            _title = UtilityBLL.ReplaceSpaceWithHyphin_v2(_title.Trim().ToLower());

            return Config.GetUrl("wiki/" + _title);
        }
    }
}

/*
 * This file is subject to the terms and conditions defined in
 * file 'LICENSE.md', which is part of this source code package.
 * Copyright 2007 - 2020 MediaSoftPro
 * For more information email at support@mediasoftpro.com
 */
