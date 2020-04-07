using Jugnoon.BLL;
using Jugnoon.Entity;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Options;
using Jugnoon.Utility;
using Microsoft.Extensions.Caching.Memory;
using Jugnoon.Framework;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Jugnoon.Settings;
using Jugnoon.Localize;

namespace DictionaryEngine.Areas.api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class wikiController : ControllerBase
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
            // readable settigns (global)
            Configs.GeneralSettings = generalSettings.Value;
            Configs.FeatureSettings = featureSettings.Value;

            SiteConfig.Config = settings.Value;
            SiteConfig.Cache = memoryCache;
            _context = context;
           
             SiteConfig.generalLocalizer = generalLocalizer;
            SiteConfig.Environment = _environment;
            SiteConfig.HttpContextAccessor = _httpContextAccessor;
        }
        [HttpPost("load")]
        public async Task<ActionResult> load()
        {
            var json = new StreamReader(Request.Body).ReadToEnd();
            var data = JsonConvert.DeserializeObject<WikiEntity>(json);
            // disable to load complete list for admin use
            data.issummary = false;
            data.isdropdown = false;

            var _posts = await WikiBLLC.LoadItems(_context, data);
            
            var _records = 0;
            if (data.id == 0)
                _records = await WikiBLLC.Count(_context, data);

            return Ok(new { posts = _posts, records = _records });
        }

        [HttpPost("getinfo")]
        public async Task<ActionResult> getinfo()
        {
            var json = new StreamReader(Request.Body).ReadToEnd();
            var data = JsonConvert.DeserializeObject<WikiEntity>(json);

            var _posts = await WikiBLLC.LoadItems(_context, data);
            if (_posts.Count > 0)
            {
                if (data.detailview)
                    return Ok(new { status = "success", post = _posts });
                else
                    return Ok(new { status = "success", post = _posts[0] });
            }
            else
            {
                return Ok(new { status = "error", message = SiteConfig.generalLocalizer["_no_records"].Value });
            }
            
        }

        [HttpPost("proc")]
        public ActionResult proc()
        {
            var json = new StreamReader(Request.Body).ReadToEnd();
            var model = JsonConvert.DeserializeObject<JGN_Wiki>(json);

            model =  WikiBLLC.Add(_context, model);

            return Ok(new { status = "success", record= model, message = SiteConfig.generalLocalizer["_records_processed"].Value });

        }

        [HttpPost("action")]
        public async Task<ActionResult> action()
        {
            var json = new StreamReader(Request.Body).ReadToEnd();
            var data = JsonConvert.DeserializeObject<List<WikiEntity>>(json);
            
            await WikiBLLC.ProcessAction(_context, data);

            return Ok(new { status = "success", message = SiteConfig.generalLocalizer["_records_processed"].Value });
        }


    }
}

/*
 * This file is subject to the terms and conditions defined in
 * file 'LICENSE.md', which is part of this source code package.
 * Copyright 2007 - 2020 MediaSoftPro
 * For more information email at support@mediasoftpro.com
 */
