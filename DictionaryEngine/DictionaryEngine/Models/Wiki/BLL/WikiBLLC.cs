using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using System.Text.RegularExpressions;
using Jugnoon.Entity;
using Jugnoon.Utility;
using System.Linq;
using Jugnoon.Framework;
using Jugnoon.Settings;
using Microsoft.EntityFrameworkCore;
using LinqKit;
using System.Threading.Tasks;

namespace Jugnoon.BLL
{
    public class WikiBLLC
    {
        public static JGN_Wiki Add(ApplicationDbContext context, JGN_Wiki entity)
        {
            // string content = UtilityBLL.Add_NoFollow_Tag(entity.description);
            string content = Scripts.UGeneral.SanitizeText(entity.description, false);
            content = Process_WikiLinks_v2(context, content);
            if (entity.id == 0)
            {
                var _entity = new JGN_Wiki()
                {
                    userid = entity.userid,
                    replyid = entity.replyid,
                    tags = entity.tags,
                    term = entity.term,
                    term_complete = entity.term_complete,
                    description = content,
                    created_at = DateTime.Now,

                };
                context.Entry(_entity).State = EntityState.Added;

                context.SaveChanges();

                entity.id = _entity.id;
            }
            else
            {
                var item = context.JGN_Wiki
                    .Where(p => p.id == entity.id)
                    .FirstOrDefault();

                if (item != null)
                {
                    item.term = entity.term;
                    item.term_complete = entity.term_complete;
                    item.description = content;
                    item.tags = entity.tags;
                    item.updated_at = DateTime.Now;
                    context.SaveChanges();
                }

            }
            
            return entity;
            
        }

        public static async Task<bool> Delete(ApplicationDbContext context, int id)
        {
            var entity = new JGN_Wiki { id = id };
            context.JGN_Wiki.Attach(entity);
            context.JGN_Wiki.Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }

        public static Task<List<JGN_Wiki>> Fetch_Record(ApplicationDbContext context,string term)
        {
            return context.JGN_Wiki
                .Where(p => p.term == term)
                .Select(p => new JGN_Wiki()
                {
                    id = p.id,
                    term = p.term,
                    term_complete = p.term_complete,
                    description = p.description
                })
                .ToListAsync();
        }
           
        public static Task<List<WikiEntity>> Load_Wiki_Terms(ApplicationDbContext context)
        {
            return context.JGN_Wiki
                .Select(p => new WikiEntity()
                {
                    term = p.term,
                    term_complete = p.term_complete
                })
                .ToListAsync();
        }

        public static string Process_WikiLinks_v2(ApplicationDbContext context, string text)
        {
            var _lst = WikiBLLC.Load_Wiki_Terms(context).Result;
            if (_lst.Count == 0)
                return text;
            if (_lst.Count > 0)
            {
                int i = 0;
                string keywords = "";
                for (i = 0; i <= _lst.Count - 1; i++)
                {
                    var term = _lst[i].term;
                    if (_lst[i].term_complete != null)
                        term = _lst[i].term_complete;
                    if (term != null && term.Length > 3)
                    {
                        keywords = @"(?<hrefurl><a[^>]*>.*?</a>)|(?<term>(\b" + term + @"\b))";
                        text = Regex.Replace(text, keywords, new MatchEvaluator(MatchEval));
                    }
                    /*if (_lst[i].term.Length > 3 && _lst[i].term_complete.Length > 3)
                    {

                        //Regex r = new Regex(", ?");
                        if (_lst[i].term_complete == _lst[i].term)
                            keywords = @"(?<hrefurl><a[^>]*>.*?</a>)|(?<term>(\b" + _lst[i].term.Trim().ToLower() + @"\b))";
                        else
                            keywords = @"(?<hrefurl><a[^>]*>.*?</a>)|(?<term>(\b" + _lst[i].term_complete.Trim().ToLower() + @"\b|\b" + _lst[i].term.Trim().ToLower() + @"\b))";
                        //keywords = @"(@<hrefurl><a[^>]*>.*?</a>)|(@<term>" + _lst[i].term.Trim().ToLower() + ")";
                        text = Regex.Replace(text, keywords, new MatchEvaluator(MatchEval));
                    }*/
                }
            }
            return text;
        }

        public static string MatchEval(Match match)
        {
            if (match.Groups["term"].Success)
            {
                var url = WikiUrlConfig.PrepareUrl(new JGN_Wiki()
                {
                    term = match.Groups["term"].Value.Trim().ToLower()
                });
                return "<a href=\"" + url + "\" title=\"" + match.Groups["term"].Value.ToString().Trim() + "\">" + match.Groups["term"].Value.ToString() + "</a>";
            }
            else
            {
                // no match
                return match.Groups["hrefurl"].Value;
            }
        }

 
        #region Core Loading Script

        public static Task<List<JGN_Wiki>> LoadItems(ApplicationDbContext context,WikiEntity entity)
        {
            if (!entity.iscache 
                || Configs.GeneralSettings.cache_duration == 0 
                || entity.pagenumber > Configs.GeneralSettings.max_cache_pages)
            {
                return FetchItems(context,entity);
            }
            else
            {
                string key = GenerateKey("lg_wiki_", entity);
                var data = new List<JGN_Wiki>();
                if (!SiteConfig.Cache.TryGetValue(key, out data))
                {
                    data = FetchItems(context,entity).Result;

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromSeconds(3600));

                    // Save data in cache.
                    SiteConfig.Cache.Set(key, data, cacheEntryOptions);
                }
                else
                {
                    data = (List<JGN_Wiki>)SiteConfig.Cache.Get(key);
                }

                return Task.Run(() => data);
            }
        }


        private static Task<List<JGN_Wiki>> FetchItems(ApplicationDbContext context,WikiEntity entity)
        {
            var collectionQuery = context.JGN_Wiki.Where(returnWhereClause(entity));
            collectionQuery = processOptionalConditions(collectionQuery, entity);
            if (entity.id > 0)
                return LoadCompleteList(collectionQuery);
            else if (entity.isdropdown)
                return LoadDropdownList(collectionQuery);
            else
                return LoadSummaryList(collectionQuery);
        }
        public static Task<int> Count(ApplicationDbContext context,WikiEntity entity)
        {
            if (!entity.iscache 
                || Configs.GeneralSettings.cache_duration == 0  
                || entity.pagenumber > Configs.GeneralSettings.max_cache_pages)
            {
                return CountRecords(context, entity);
            }
            else
            {
                string key = GenerateKey("cnt_wiki", entity);
                int records = 0;
                if (!SiteConfig.Cache.TryGetValue(key, out records))
                {
                    records = CountRecords(context, entity).Result;

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromSeconds(3600));

                    // Save data in cache.
                    SiteConfig.Cache.Set(key, records, cacheEntryOptions);
                }
                else
                {
                    records = (int)SiteConfig.Cache.Get(key);
                }
                return Task.Run(() => records);
            }
        }

        private static Task<int> CountRecords(ApplicationDbContext context,WikiEntity entity)
        {
            return context.JGN_Wiki.Where(returnWhereClause(entity)).CountAsync();
        }

        private static string GenerateKey(string key, WikiEntity entity)
        {
            return key + UtilityBLL.ReplaceSpaceWithUnderscore(entity.term_complete) + 
                UtilityBLL.ReplaceSpaceWithUnderscore(entity.term) + "" + 
                UtilityBLL.ReplaceSpaceWithHyphin(entity.order.ToLower()) + "" + entity.pagenumber + "" + entity.term;
        }
        private static Task<List<JGN_Wiki>> LoadCompleteList(IQueryable<JGN_Wiki> query)
        {
            return query.Select(p => new JGN_Wiki
            {
                id = p.id,
                replyid = p.replyid,
                userid = p.userid,
                created_at = p.created_at,
                updated_at = p.updated_at,
                tags = p.tags,
                term = p.term,
                term_complete = p.term_complete,
                description = p.description
            }).ToListAsync();
        }
        private static Task<List<JGN_Wiki>> LoadSummaryList(IQueryable<JGN_Wiki> query)
        {
            return query.Select(p => new JGN_Wiki
            {
                id = p.id,
                term = p.term,
                term_complete = p.term_complete,
                tags = p.tags,
                description = p.description
            }).ToListAsync();
        }
        private static Task<List<JGN_Wiki>> LoadDropdownList(IQueryable<JGN_Wiki> query)
        {
            return query.Select(p => new JGN_Wiki
            {
                id = p.id,
                term = p.term,
                term_complete = p.term_complete
            }).ToListAsync();
        }

        private static IQueryable<JGN_Wiki> processOptionalConditions(IQueryable<JGN_Wiki> collectionQuery, WikiEntity query)
        {
            if (query.order != "")
                collectionQuery = (IQueryable<JGN_Wiki>)collectionQuery.Sort(query.order);

            if (query.id == 0)
            {
                // skip logic
                if (query.pagenumber > 1)
                    collectionQuery = collectionQuery.Skip(query.pagesize * (query.pagenumber - 1));
                // take logic
                if (!query.loadall)
                    collectionQuery = collectionQuery.Take(query.pagesize);
            }

            return collectionQuery;
        }
      
        private static System.Linq.Expressions.Expression<Func<JGN_Wiki, bool>> returnWhereClause(WikiEntity entity)
        {
            var where_clause = PredicateBuilder.New<JGN_Wiki>(true);
           
            if (entity.excludedid > 0)
                where_clause = where_clause.And(p => p.id != entity.excludedid);

            if (entity.id > 0 && entity.detailview)
                where_clause = where_clause.And(p => p.id == entity.id || p.replyid == entity.id);
            else if (entity.id > 0 && !entity.detailview)
                where_clause = where_clause.And(p => p.id == entity.id);

            if (entity.userid != "")
                where_clause = where_clause.And(p => p.userid == entity.userid);

            if (entity.replyid > 0)
                where_clause = where_clause.And(p => p.replyid == entity.replyid);
            else if (!entity.detailview)
                where_clause = where_clause.And(p => p.replyid == 0);

            if (entity.term != null && entity.term != "")
                where_clause = where_clause.And(p => p.term_complete.Contains(entity.term) || p.term.Contains(entity.term));

            if (entity.term_complete != null && entity.term_complete != "")
                where_clause = where_clause.And(p => p.term_complete.Contains(entity.term_complete) || p.term.Contains(entity.term));

            if (entity.character != null && entity.character != "")
            {
                where_clause = where_clause.And(p => p.term_complete.StartsWith(entity.character) || p.term.StartsWith(entity.character));
            }

            if (entity.month > 0 && entity.year > 0)
                where_clause = where_clause.And(p => p.created_at.Value.Month == entity.month && p.created_at.Value.Year == entity.year);
            else if (entity.year > 0)
                where_clause = where_clause.And(p => p.created_at.Value.Year == entity.year);
            else if (entity.month > 0)
                where_clause = where_clause.And(p => p.created_at.Value.Month == entity.month);

            if (entity.tags != "")
                where_clause = where_clause.And(p => p.tags.Contains(entity.tags.ToString()));

            return where_clause;
        }

        #endregion

        /// <summary>
        ///  Generate dynamic wiki posts sitemap for search engine submissions
        /// </summary>
        public static async Task<string> BuildGoogleSiteMapUrls(ApplicationDbContext context)
        {
            var str = new StringBuilder();
            str.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"  xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\">");
            // start sitemap url
            var _items = await LoadItems(context,new WikiEntity()
            {
                pagesize = 50000
            });
            foreach(var item in _items)
            {
                str.AppendLine("<url>");
                str.AppendLine("<loc>" + WikiUrlConfig.PrepareUrl(item) + "</loc>");
                str.AppendLine("</url>");
            }
            // close sitemap url
            str.AppendLine("</urlset>");

            return str.ToString();
        }

        public static async Task<string> ProcessAction(ApplicationDbContext context,List<WikiEntity> list)
        {
            foreach (var entity in list)
            {
                if (entity.id > 0)
                {
                    switch (entity.actionstatus)
                    {                       
                        case "delete":
                            await Delete(context, (int)entity.id);
                            break;
                    }
                }
            }
            return "OK";
        }

    }
}

/*
 * This file is subject to the terms and conditions defined in
 * file 'LICENSE.md', which is part of this source code package.
 * Copyright 2007 - 2020 MediaSoftPro
 * For more information email at support@mediasoftpro.com
 */
