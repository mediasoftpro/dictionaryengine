using Microsoft.AspNetCore.Mvc;
using Jugnoon.Utility;
using Jugnoon.Entity;
using Jugnoon.BLL;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Jugnoon.Framework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Ganss.XSS;
using System.Threading.Tasks;
using Jugnoon.Settings;
using Jugnoon.Models;
using Jugnoon.Localize;

namespace DictionaryEngine.Controllers
{
    public class glossaryController : Controller
    {
        ApplicationDbContext _context;
        public glossaryController(
           IOptions<SiteConfiguration> settings,
           IMemoryCache memoryCache,
           ApplicationDbContext context,
           IStringLocalizer<GeneralResource> generalLocalizer,
           IStringLocalizer<DictionaryResource> dictionaryLocalizer,
           IWebHostEnvironment _environment,
           IHttpContextAccessor _httpContextAccessor,
           IOptions<General> generalSettings,
           IOptions<Features> featureSettings
           )
        {

            _context = context;
            // readable settings (global)
            Configs.GeneralSettings = generalSettings.Value;
            Configs.FeatureSettings = featureSettings.Value;

            SiteConfig.Config = settings.Value;
            SiteConfig.Cache = memoryCache;
            SiteConfig.generalLocalizer = generalLocalizer;
            SiteConfig.dictionaryLocalizer = dictionaryLocalizer;
            SiteConfig.Environment = _environment;
            SiteConfig.HttpContextAccessor = _httpContextAccessor;
        }
        // GET: glossary
        public async Task<IActionResult> Index(int? id)
        {
            int pagenumber = 1;
            if (id != null)
                pagenumber = (int)id;

            /* List Initialization */
            var ListEntity = new GlossaryListView()
            {
                isListStatus = false,
                QueryOptions = new WikiEntity()
                {
                    pagenumber = (int)pagenumber,
                    term = "",
                    iscache = true,
                    pagesize = 20,
                    order = "term asc",
                },
                DefaultUrl = Config.GetUrl("glossary/"),
                PaginationUrl = Config.GetUrl("glossary/Index/[p]/"),
                NoRecordFoundText = SiteConfig.generalLocalizer["_no_records"].Value,
            };

            if (HttpContext.Request.Query["cq"].Count > 0)
                ListEntity.Character = HttpContext.Request.Query["cq"].ToString();
            if (HttpContext.Request.Query["query"].Count > 0)
                ListEntity.QueryOptions.term = HttpContext.Request.Query["query"].ToString();

            ListEntity.TotalRecords = await WikiBLLC.Count(_context, ListEntity.QueryOptions);
            if (ListEntity.TotalRecords > 0)
            {
                ListEntity.DataList = await WikiBLLC.LoadItems(_context, ListEntity.QueryOptions);
            }
            
            if (ListEntity.Character != "" && ListEntity.Character != null)
            {
                ViewBag.title = SiteConfig.dictionaryLocalizer["_wiki_posts_filter_character"].Value + " " + ListEntity.Character.ToUpper();
            }
            else if (ListEntity.QueryOptions.term != "" && ListEntity.QueryOptions.term != null)
            {
                ViewBag.title = ListEntity.QueryOptions.term + " | " + SiteConfig.dictionaryLocalizer["_wiki_posts"].Value;
            }
            else
            {
                ViewBag.title = SiteConfig.dictionaryLocalizer["_wiki_posts"].Value;
            }

            return View(ListEntity);
        }

        public async Task<IActionResult> character(string term, int? id)
        {
            int pagenumber = 1;
            if (id != null)
                pagenumber = (int)id;

            string _character = "";
            if (term != null)
            {
                var _sanitize = new HtmlSanitizer();
                _character = _sanitize.Sanitize(UtilityBLL.ReplaceHyphinWithSpace(term));
            }
            /* List Initialization */
            var ListEntity = new GlossaryListView()
            {
                isListStatus = false,
                Character = _character,
                QueryOptions = new WikiEntity()
                {
                    pagenumber = (int)pagenumber,
                    character = _character,
                    iscache = true,
                    pagesize = 20,
                    order = "id desc",
                },
                DefaultUrl = Config.GetUrl("glosarry/character/" + term),
                PaginationUrl = Config.GetUrl("glossary/character/" + term + "/[p]/"),
                NoRecordFoundText = SiteConfig.generalLocalizer["_no_records"].Value,
            };

            if (HttpContext.Request.Query["cq"].Count > 0)
                ListEntity.Character = HttpContext.Request.Query["cq"].ToString();
            if (HttpContext.Request.Query["query"].Count > 0)
                ListEntity.QueryOptions.term = HttpContext.Request.Query["query"].ToString();

            ListEntity.TotalRecords = await WikiBLLC.Count(_context, ListEntity.QueryOptions);
            if (ListEntity.TotalRecords > 0)
            {
                ListEntity.DataList = await WikiBLLC.LoadItems(_context, ListEntity.QueryOptions);
            }

            if (ListEntity.Character != "" && ListEntity.Character != null)
            {
                ViewBag.title = SiteConfig.dictionaryLocalizer["_wiki_posts_filter_character"].Value + " " + ListEntity.Character.ToUpper();
            }
            else if (ListEntity.QueryOptions.term != "" && ListEntity.QueryOptions.term != null)
            {               
                ViewBag.title = ListEntity.QueryOptions.term + " | " + SiteConfig.dictionaryLocalizer["_wiki_posts"].Value;
            }
            else
            {
                ViewBag.title = SiteConfig.dictionaryLocalizer["_wiki_posts"].Value;
            }

            return View(ListEntity);
        }

        public async Task<IActionResult> term(string term, int? id)
        {
            int pagenumber = 1;
            if (id != null)
                pagenumber = (int)id;

            var _sanitize = new HtmlSanitizer();
            term = _sanitize.Sanitize(UtilityBLL.ReplaceHyphinWithSpace(term));
            /* List Initialization */
            var ListEntity = new GlossaryListView()
            {
                isListStatus = false,
                QueryOptions = new WikiEntity()
                {
                    pagenumber = (int)pagenumber,
                    term = term,
                    iscache = false,
                    pagesize = 20,
                    order = "id desc",
                    
                },
                ListObject = new Jugnoon.Scripts.ListItems()
                {
                    ListType = Jugnoon.Scripts.ListType.List, // 0: grid 1: list
                },
                DefaultUrl = Config.GetUrl("glosarry/term/" + term),
                PaginationUrl = Config.GetUrl("glossary/term/" + term + "/[p]/"),
                NoRecordFoundText = SiteConfig.generalLocalizer["_no_records"].Value,
            };
                        
            if (HttpContext.Request.Query["query"].Count > 0)
                ListEntity.QueryOptions.term = HttpContext.Request.Query["query"].ToString();

            ListEntity.TotalRecords = await WikiBLLC.Count(_context, ListEntity.QueryOptions);
            if (ListEntity.TotalRecords > 0)
            {
                ListEntity.DataList = await WikiBLLC.LoadItems(_context, ListEntity.QueryOptions);
            }
            if (ListEntity.Character != "" && ListEntity.Character != null)
            {
                ViewBag.title= SiteConfig.dictionaryLocalizer["glossary_character_title"].ToString().Replace("{CN}", ListEntity.Character.ToLower());
                ViewBag.description = SiteConfig.dictionaryLocalizer["glossary_character_meta"].ToString().Replace("{CN}", ListEntity.Character.ToLower());
            }
            else if (ListEntity.QueryOptions.term != "" && ListEntity.QueryOptions.term != null)
            {
                ViewBag.title= SiteConfig.dictionaryLocalizer["glossary_query_title"].ToString().Replace("{CN}", ListEntity.QueryOptions.term.ToLower());
            }
            else
            {
                ViewBag.title= SiteConfig.dictionaryLocalizer["glossary_home_title"];
                ViewBag.description = SiteConfig.dictionaryLocalizer["glossary_home_meta"];
            }

            return View(ListEntity);
        }

        #region Sitemaps

        public async Task<IActionResult> sitemap(int? rt)
        {
            int responsetype = 0; // 0: google, 1: bing
            string sXml = "";
            if (rt != null)
                responsetype = (int)rt;
            switch (responsetype)
            {
                case 0:
                    sXml = await WikiBLLC.BuildGoogleSiteMapUrls(_context);
                    break;
            }

            return this.Content(sXml, "text/xml");
        }

        #endregion

    }
}

/*
 * This file is subject to the terms and conditions defined in
 * file 'LICENSE.md', which is part of this source code package.
 * Copyright 2007 - 2020 MediaSoftPro
 * For more information email at support@mediasoftpro.com
 */
