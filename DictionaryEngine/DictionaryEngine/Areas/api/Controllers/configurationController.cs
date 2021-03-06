﻿/*
 * This class is responsible for gathering, sending and update all injectable configuration data (handling almost whole application and additional modules)
*/

using Jugnoon.Settings;
using Jugnoon.Setup;
using Jugnoon.Utility;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.IO;
using System;
using Jugnoon.BLL;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Jugnoon.Models;

namespace DictionaryEngine.Areas.api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [System.Obsolete]
    public class configurationController : ControllerBase
    {
        // Injectors for Writable (General) Configurations
        private IApplicationLifetime ApplicationLifetime { get; set; }
        private readonly IWritableOptions<General> _general_options;
        private readonly IWritableOptions<Smtp> _smtp_options;
        private readonly IWritableOptions<Media> _media_options;
        private readonly IWritableOptions<Features> _features_options;
        private readonly IWritableOptions<Listing> _listings_options;
        private readonly IWritableOptions<Authentication> _authentication_options;
        private readonly IWritableOptions<Registration> _registration_options;
        private readonly IWritableOptions<Aws> _aws_options;
        private readonly IWritableOptions<Social> _social_options;
        private readonly IWritableOptions<Contact> _contact_options;
        private readonly IWritableOptions<Database> _database_options;
        private readonly IWritableOptions<Rechapcha> _rechapcha_options;

        // Injectors for Reading Configurations
        General _generalSettings;
        Smtp _smtpSettings;
        Media _mediaSettings;
        Features _featureSettings;
        Listing _listingSettings;
        Authentication _authenticationSettings;
        Registration _registrationSettings;
        Aws _awsSettings;
        Social _socialSettings;
        Contact _contactSettings;
        Rechapcha _rechapchaSettings;

        public configurationController(
            IApplicationLifetime applicationLifetime,
            IWritableOptions<General> general_options,
            IWritableOptions<Database> database_options,
            IWritableOptions<Smtp> smtp_options,
            IWritableOptions<Media> media_options,
            IWritableOptions<Features> features_options,
            IWritableOptions<Listing> listings_options,
            IWritableOptions<Authentication> authentication_options,
            IWritableOptions<Registration> registration_options,
            IWritableOptions<Aws> aws_options,
            IWritableOptions<Social> social_options,
            IWritableOptions<Contact> contact_options,
            IWritableOptions<Rechapcha> rechapcha_options,
            IOptions<General> generalSettings,
            IOptions<Smtp> smtpSettings,
            IOptions<Media> mediaSettings,
            IOptions<Features> featureSettings,
            IOptions<Listing> listingSettings,
            IOptions<Authentication> authenticationSettings,
            IOptions<Registration> registrationSettings,
            IOptions<Aws> awsSettings,
            IOptions<Social> socialSettings,
            IOptions<Contact> contactSettings,
            IOptions<Rechapcha> rechapchaSettings
            
         )
        {
            // writable injectors
            _database_options = database_options;
            _general_options = general_options;
            _smtp_options = smtp_options;
            _media_options = media_options;
            _features_options = features_options;
            _listings_options = listings_options;
            _authentication_options = authentication_options;
            _registration_options = registration_options;
            _aws_options = aws_options;
            _social_options = social_options;
            _contact_options = contact_options;
            _rechapcha_options = rechapcha_options;
            ApplicationLifetime = applicationLifetime;
            // readable injectors
            _generalSettings = generalSettings.Value;
            _smtpSettings = smtpSettings.Value;
            _mediaSettings = mediaSettings.Value;
            _featureSettings = featureSettings.Value;
            _listingSettings = listingSettings.Value;
            _authenticationSettings = authenticationSettings.Value;
            _registrationSettings = registrationSettings.Value;
            _awsSettings = awsSettings.Value;
            _socialSettings = socialSettings.Value;
            _contactSettings = contactSettings.Value;
            _rechapchaSettings = rechapchaSettings.Value;
        }

        [HttpPost("load")]
        public ActionResult load()
        {
            return Ok(new
            {
                configurations = new
                {
                    general = new
                    {
                        general = _generalSettings,
                        smtp = _smtpSettings,
                        media = _mediaSettings,
                        features = _featureSettings,
                        listings = _listingSettings,
                        authentication = _authenticationSettings,
                        registration = _registrationSettings,
                        aws = _awsSettings,
                        social = _socialSettings,
                        contact = _contactSettings,
                        rechapcha = _rechapchaSettings
                    }
                }
            });
        }

        [HttpPost("load_settings")]
        public ActionResult load_settings()
        {
            // Pass only settings may be needed in front application.
            return Ok(new
            {
                configurations = new
                {
                    general = new
                    {
                        //general = _generalSettings,
                        features = _featureSettings,
                        listings = _listingSettings,
                        media = _mediaSettings,
                        category = ((CategoryBLL.Types[])Enum.GetValues(typeof(CategoryBLL.Types))).ToDictionary(k => k.ToString(), v => (int)v),
                        mailtemplates = ((MailTemplateTypes[])Enum.GetValues(typeof(MailTemplateTypes))).ToDictionary(k => k.ToString(), v => (int)v)
                    }
                }
            });
        }

        #region Update Configuration API Calls


        [HttpPost("dbsetup")]
        public ActionResult dbsetup()
        {
            var json = new StreamReader(Request.Body).ReadToEnd();
            if (json == "")
            {
                return Ok(new { status = "Error", message = SiteConfig.generalLocalizer["_invalid_data"].Value });
            }
            var data = JsonConvert.DeserializeObject<Database>(json);

            _database_options.Update(opt => {
                opt.host = data.host;
                opt.database = data.database;
                opt.userid = data.userid;
                opt.password = data.password;
            });

            //ApplicationLifetime.StopApplication();

            return Ok(new
            {
                status = 200
            });
        }

        [HttpPost("dbsetupcompleted")]
        public ActionResult dbsetupcompleted()
        {
            // setup completed, please restart your application to effect configuration
            ApplicationLifetime.StopApplication();

            return Ok(new
            {
                status = 200
            });
        }

        [HttpPost("general")]
        public ActionResult general()
        {
            var json = new StreamReader(Request.Body).ReadToEnd();
            if (json == "")
            {
                return Ok(new { status = "Error", message = SiteConfig.generalLocalizer["_invalid_data"].Value });
            }
            var data = JsonConvert.DeserializeObject<General>(json);

            _general_options.Update(opt => {
                opt.site_theme = data.site_theme;
                opt.website_title = data.website_title;
                opt.website_description = UtilityBLL.processNull(data.website_description, 0);
                opt.page_caption = UtilityBLL.processNull(data.page_caption, 0);
                opt.website_phone = UtilityBLL.processNull(data.website_phone, 0);
                opt.admin_mail = UtilityBLL.processNull(data.admin_mail, 0);
                opt.admin_mail_name = UtilityBLL.processNull(data.admin_mail_name, 0);
                opt.pagination_links = data.pagination_links;
                opt.pagination_type = data.pagination_type;
                opt.screen_content = data.screen_content;
                opt.content_approval = data.content_approval;
                if (data.spam_count > 0)
                    opt.spam_count = data.spam_count;

                opt.cache_duration = data.cache_duration;

                if (data.max_cache_pages > 0)
                    opt.max_cache_pages = data.max_cache_pages;

                opt.store_searches = data.store_searches;
                opt.store_ipaddress = data.store_ipaddress;
                opt.maximum_dynamic_link_length = data.maximum_dynamic_link_length;
                opt.default_culture = UtilityBLL.processNull(data.default_culture, 0);
                if (opt.pagesize > 0)
                    opt.pagesize = data.pagesize;
                opt.rating_option = data.rating_option;
                opt.jwt_private_key = data.jwt_private_key;
                opt.init_wiz = true; // used to stop installation process after application deployment, don't remove it
            });

            //ApplicationLifetime.StopApplication();

            return Ok(new
            {
                status = 200
            });
        }


        [HttpPost("media")]
        public ActionResult media()
        {
            var json = new StreamReader(Request.Body).ReadToEnd();
            if (json == "")
            {
                return Ok(new { status = "Error", message = SiteConfig.generalLocalizer["_invalid_data"].Value });
            }
            var data = JsonConvert.DeserializeObject<Media>(json);

            _media_options.Update(opt => {
                opt.user_thumbnail_width = data.user_thumbnail_width;
                opt.user_thumbnail_height = data.user_thumbnail_height;
                opt.category_thumbnail_width = data.category_thumbnail_width;
                opt.category_thumbnail_height = data.category_thumbnail_height;
                opt.gamify_badge_height = data.gamify_badge_height;
                opt.gamify_badge_width = data.gamify_badge_width;
                opt.photo_extensions = data.photo_extensions;
                opt.photo_max_size = data.photo_max_size;
                opt.quality = data.quality;
                opt.logo_path = data.logo_path;
                opt.user_default_path = data.user_default_path;
                opt.category_default_path = data.category_default_path;
                opt.gamify_default_path = data.gamify_default_path;
            });

            //ApplicationLifetime.StopApplication();

            return Ok(new
            {
                status = 200
            });
        }

        [HttpPost("features")]
        public ActionResult features()
        {
            var json = new StreamReader(Request.Body).ReadToEnd();
            if (json == "")
            {
                return Ok(new { status = "Error", message = SiteConfig.generalLocalizer["_invalid_data"].Value });
            }
            var data = JsonConvert.DeserializeObject<Features>(json);

            _features_options.Update(opt => {
               
                opt.enable_wiki_glossary = data.enable_wiki_glossary;
                opt.enable_categories = data.enable_categories;
                opt.enable_tags = data.enable_tags;
                opt.showLabelCounter = data.showLabelCounter;
                opt.enable_archives = data.enable_archives;
                opt.enable_date_filter = data.enable_date_filter;
                opt.enable_advertisement = data.enable_advertisement;
                opt.enable_adult_veritifcation = data.enable_adult_veritifcation;
                opt.enable_languages = data.enable_languages;
            });

            //ApplicationLifetime.StopApplication();

            return Ok(new
            {
                status = 200
            });
        }

        [HttpPost("listings")]
        public ActionResult listings()
        {
            var json = new StreamReader(Request.Body).ReadToEnd();
            if (json == "")
            {
                return Ok(new { status = "Error", message = SiteConfig.generalLocalizer["_invalid_data"].Value });
            }
            var data = JsonConvert.DeserializeObject<Listing>(json);

            _listings_options.Update(opt => {
                opt.enable_views = data.enable_views;
                opt.enable_date = data.enable_date;
                opt.enable_username = data.enable_username;
                opt.enable_rating = data.enable_rating;
                opt.enable_likedislike = data.enable_likedislike;
            });

            //ApplicationLifetime.StopApplication();

            return Ok(new
            {
                status = 200
            });
        }

        [HttpPost("authentication")]
        public ActionResult authentication()
        {
            var json = new StreamReader(Request.Body).ReadToEnd();
            if (json == "")
            {
                return Ok(new { status = "Error", message = SiteConfig.generalLocalizer["_invalid_data"].Value });
            }
            var data = JsonConvert.DeserializeObject<Authentication>(json);

            _authentication_options.Update(opt => {
                opt.enable_facebook = data.enable_facebook;
                opt.fb_appId = data.fb_appId;
                opt.fb_appSecrete = data.fb_appSecrete;
                opt.enable_twitter = data.enable_twitter;
                opt.tw_consumer_key = data.tw_consumer_key;
                opt.tw_consumer_secrete = data.tw_consumer_secrete;
                opt.enable_google = data.enable_google;
                opt.google_clientid = data.google_clientid;
                opt.google_clientsecrete = data.google_clientsecrete;
            });

            // ApplicationLifetime.StopApplication();

            return Ok(new
            {
                status = 200
            });
        }

        [HttpPost("registration")]
        public ActionResult registration()
        {
            var json = new StreamReader(Request.Body).ReadToEnd();
            if (json == "")
            {
                return Ok(new { status = "Error", message = SiteConfig.generalLocalizer["_invalid_data"].Value });
            }
            var data = JsonConvert.DeserializeObject<Registration>(json);

            _registration_options.Update(opt => {
                opt.enable = data.enable;
                opt.enableChapcha = data.enableChapcha;
                opt.uniqueFieldOption = data.uniqueFieldOption;
                opt.enableNameField = data.enableNameField;
                opt.enablePrivacyCheck = data.enablePrivacyCheck;
            });

            // ApplicationLifetime.StopApplication();

            return Ok(new
            {
                status = 200
            });
        }

        [HttpPost("aws")]
        public ActionResult aws()
        {
            var json = new StreamReader(Request.Body).ReadToEnd();
            if (json == "")
            {
                return Ok(new { status = "Error", message = SiteConfig.generalLocalizer["_invalid_data"].Value });
            }
            var data = JsonConvert.DeserializeObject<Aws>(json);

            _aws_options.Update(opt => {
                opt.enable = data.enable;
                opt.accessKey = data.accessKey;
                opt.secretKey = data.secretKey;
                opt.region = data.region;
                opt.bucket = data.bucket;
                opt.user_photos_directory = data.user_photos_directory;
                opt.category_photos_directory = data.category_photos_directory;
                opt.gamify_badges_directory = data.gamify_badges_directory;
                opt.cdn_URL = data.cdn_URL;
            });

            // ApplicationLifetime.StopApplication();

            return Ok(new
            {
                status = 200
            });
        }

        [HttpPost("social")]
        public ActionResult social()
        {
            var json = new StreamReader(Request.Body).ReadToEnd();
            if (json == "")
            {
                return Ok(new { status = "Error", message = SiteConfig.generalLocalizer["_invalid_data"].Value });
            }
            var data = JsonConvert.DeserializeObject<Social>(json);

            _social_options.Update(opt => {
                opt.facebook_url = data.facebook_url;
                opt.twitter_url = data.twitter_url;
                opt.flickr_url = data.flickr_url;
                opt.linkedin_url = data.linkedin_url;
                opt.thumblr_url = data.thumblr_url;
                opt.google_url = data.google_url;
                opt.youtube_url = data.youtube_url;
                opt.vimeo_url = data.vimeo_url;
                opt.pinterest_url = data.pinterest_url;
                opt.instagram_url = data.instagram_url;
                opt.github_url = data.github_url;
                opt.rss_url = data.rss_url;
                opt.addthis_pubid = data.addthis_pubid;
                opt.sharethis_propertyId = data.sharethis_propertyId;
                opt.fb_appId = data.fb_appId;
            });

            // ApplicationLifetime.StopApplication();

            return Ok(new
            {
                status = 200
            });
        }

        [HttpPost("contact")]
        public ActionResult contact()
        {
            var json = new StreamReader(Request.Body).ReadToEnd();
            if (json == "")
            {
                return Ok(new { status = "Error", message = SiteConfig.generalLocalizer["_invalid_data"].Value });
            }
            var data = JsonConvert.DeserializeObject<Contact>(json);

            _contact_options.Update(opt => {
                opt.address = data.address;
                opt.tel1 = data.tel1;
                opt.tel2 = data.tel2;
                opt.fax = data.fax;
                opt.email = data.email;
                opt.detail_info = data.detail_info;
                opt.enable_contact_form = data.enable_contact_form;
            });

            // ApplicationLifetime.StopApplication();

            return Ok(new
            {
                status = 200
            });
        }

        [HttpPost("smtp")]
        public ActionResult smtp()
        {
            var json = new StreamReader(Request.Body).ReadToEnd();
            if (json == "")
            {
                return Ok(new { status = "Error", message = SiteConfig.generalLocalizer["_invalid_data"].Value });
            }
            var data = JsonConvert.DeserializeObject<Smtp>(json);

            _smtp_options.Update(opt => {
                opt.enable_email = data.enable_email;
                opt.enable_mandril = data.enable_mandril;
                opt.mandril_key = data.mandril_key;
                opt.enable_SES = data.enable_SES;
                opt.ses_host = data.ses_host;
                opt.ses_username = data.ses_username;
                opt.ses_password = data.ses_password;
                opt.Server = data.Server;
                opt.Port = data.Port;
                opt.FromAddress = data.FromAddress;
            });

            // ApplicationLifetime.StopApplication();

            return Ok(new
            {
                status = 200
            });
        }

        [HttpPost("rechapcha")]
        public ActionResult rechapcha()
        {
            var json = new StreamReader(Request.Body).ReadToEnd();
            if (json == "")
            {
                return Ok(new { status = "Error", message = SiteConfig.generalLocalizer["_invalid_data"].Value });
            }
            var data = JsonConvert.DeserializeObject<Rechapcha>(json);

            _rechapcha_options.Update(opt => {
                opt.SiteKey = data.SiteKey;
                opt.SecretKey = data.SecretKey;
                opt.Version = data.Version;
            });

            // ApplicationLifetime.StopApplication();

            return Ok(new
            {
                status = 200
            });
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
