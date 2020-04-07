using Microsoft.AspNetCore.Mvc;
using Jugnoon.BLL;
using Jugnoon.Utility;
using Jugnoon.Settings;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Jugnoon.Framework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net;
using Jugnoon.Models;
using System.Threading.Tasks;
using Jugnoon.Localize;

namespace DictionaryEngine.Controllers
{
    public class wikiController : Controller
    {
        ApplicationDbContext _context;
        public wikiController(
           IOptions<SiteConfiguration> settings,
           IMemoryCache memoryCache,
           ApplicationDbContext context,
           IStringLocalizer<GeneralResource> generalLocalizer,
           IWebHostEnvironment _environment,
           IHttpContextAccessor _httpContextAccessor,
           IOptions<General> generalSettings,
           IOptions<Features> featureSettings
           )
        {
            // database context
            _context = context;

            // general settings
            Configs.GeneralSettings = generalSettings.Value;
            Configs.FeatureSettings = featureSettings.Value;

            SiteConfig.Config = settings.Value;
            SiteConfig.Cache = memoryCache;
             SiteConfig.generalLocalizer = generalLocalizer;
            SiteConfig.Environment = _environment;
            SiteConfig.HttpContextAccessor = _httpContextAccessor;
        }

        // GET: wiki
        public async Task<IActionResult> Index(string title)
        {
            if(title == null)
            {
                return Redirect(Config.GetUrl("glossary"));
            }

            title = UtilityBLL.UppercaseFirst(UtilityBLL.ReplaceHyphinWithSpace(title));

            var model = new WikiModelView();
            model.isAllowed = true;

            var _lst = await WikiBLLC.Fetch_Record(_context, title);
            if (_lst.Count > 0)
            {
                model.Data = new JGN_Wiki()
                {
                    term_complete = _lst[0].term_complete,
                    description  = BBCode.MakeHtml(WebUtility.HtmlDecode(_lst[0].description), true)
                };
            }
            else
            {
                model.isAllowed = false;
                model.DetailMessage = SiteConfig.generalLocalizer["_no_records"].Value;
            }

            ViewBag.title = title;

            return View(model);
        }
    }
}

/*
 * This file is subject to the terms and conditions defined in
 * file 'LICENSE.md', which is part of this source code package.
 * Copyright 2007 - 2020 MediaSoftPro
 * For more information email at support@mediasoftpro.com
 */
