
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.ValueContentAnalysis;
using Newtonsoft.Json.Serialization;
using System.Linq;

namespace L00177804_Golf.Pages.Memberships
{
    public class IndexModel : PageModel
    {
        private readonly L00177804_Golf.Data.L00177804_GolfContext memberInfo;

        public IndexModel(L00177804_Golf.Data.L00177804_GolfContext memberInfo)
        {
            this.memberInfo = memberInfo;
        }

        public IList<Membership> Membership { get; set; } = default!;


        [BindProperty(SupportsGet = true)]
        public string NameSort { get; set; } = "name_desc";


        [BindProperty(SupportsGet = true)]
        public string HandiSort { get; set; } = "handi_desc";


        [BindProperty(SupportsGet = true)]
        public string CurrentFilter { get; set; } = default!;

        /// <summary>
        ///  This method is an event handler method for the HTTP GET request for a Razor page
        ///  IQueryable object that represents a query for membership data from the memberInfo object.
        ///  Executes the query using the ToListAsync() method, which retrieves the list of membership objects
        ///  from the database asynchronously and assigns them to the Membership property of the Razor page.
        /// </summary>
        /// <returns>list of memberships retrieved from the database.</returns>

        public async Task OnGetAsync()
        {
            // Get the IQueryable of Membership objects
            var membershipQuery = memberInfo.Membership.AsQueryable();

            Membership = await membershipQuery.ToListAsync();
        }

        /// <summary>
        /// Get Method to Sort List into Ascending and Descending 
        /// By Name and HandiCap
        /// </summary>
        /// <param name="sorting"></param>
        /// <returns></returns>
        public async Task OnGetSorting(string sorting)
        {
            // using System;
            NameSort = String.IsNullOrEmpty(sorting) ? "name_desc" : "";
            HandiSort = sorting == "handi_ascend" ? "handi_desc" : "handi_ascend";

            // Get the IQueryable of Membership objects
            var membershipQuery = memberInfo.Membership.AsQueryable();

            switch (sorting)
            {
                case "name_desc":
                    membershipQuery = membershipQuery.OrderByDescending(m => m.FirstName);
                    break;
                case "handi_ascend":
                    membershipQuery = membershipQuery.OrderBy(m => m.Handicap);
                    break;
                case "handi_desc":
                    membershipQuery = membershipQuery.OrderByDescending(m => m.Handicap);
                    break;
                default:
                    membershipQuery = membershipQuery.OrderBy(m => m.FirstName);
                    break;
            }
            // Get the list of memberships from the database
            Membership = await membershipQuery.ToListAsync();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task OnGetSearch()
        {
            var membershipQuery = memberInfo.Membership.AsQueryable();
            try
            {
                if (!string.IsNullOrEmpty(CurrentFilter))
                {
                    if (CurrentFilter == "Male Golfers")
                    {
                        membershipQuery = membershipQuery.Where(s => s.Gender.Contains("Male"));
                    }
                    if (CurrentFilter == "Female Golfers")
                    {
                        membershipQuery = membershipQuery.Where(s => s.Gender.Contains("Female"));
                    }
                    if (CurrentFilter == "Scratch Players")
                    {
                        membershipQuery = membershipQuery.Where(s => s.Handicap.Equals(0));
                    }
                    if (CurrentFilter == "Player Handicap less 10")
                    {
                        membershipQuery = membershipQuery.Where(n => n.Handicap >= 1 && n.Handicap <= 10);
                    }
                    if (CurrentFilter == "Player Handicap Greater than 10 and less than 20")
                    {
                        membershipQuery = membershipQuery.Where(n => n.Handicap > 10 && n.Handicap < 20);
                    }
                    if (CurrentFilter == "Player Handicap Greater 20")
                    {
                        membershipQuery = membershipQuery.Where(n => n.Handicap > 20);
                    }
                    foreach(var membership in membershipQuery)
                    {
                        if (membership.FirstName == CurrentFilter)
                        {
                            membershipQuery = membershipQuery.Where(s => s.FirstName == CurrentFilter);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            Membership = await membershipQuery.ToListAsync();
        }
    }



}

