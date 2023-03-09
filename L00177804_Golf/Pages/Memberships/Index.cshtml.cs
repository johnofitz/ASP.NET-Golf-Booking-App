
namespace L00177804_Golf.Pages.Memberships
{
    public class IndexModel : PageModel
    {
        private readonly L00177804_GolfContext memberInfo;

        public IndexModel(L00177804_GolfContext memberInfo)
        {
            this.memberInfo = memberInfo;
        }
        // List to store objects from membership database
        public IList<Membership> Membership { get; set; } = default!;

        // Sorting parameters

        [BindProperty(SupportsGet = true)]
        public string NameSort { get; set; } = "name_desc";


        [BindProperty(SupportsGet = true)]
        public string HandiSort { get; set; } = "handi_desc";

        // search string
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
        /// Method to return the search filter parameter
        /// </summary>
        /// <returns></returns>
        public async Task OnGetSearch()
        {
            var membershipQuery = memberInfo.Membership.AsQueryable();
            try
            {
                if (!string.IsNullOrEmpty(CurrentFilter))
                {
                    switch (CurrentFilter)
                    {
                        case "Male Golfers":
                            membershipQuery = membershipQuery.Where(s => s.Gender == "Male");
                            break;
                        case "Female Golfers":
                            membershipQuery = membershipQuery.Where(s => s.Gender == "Female");
                            break;
                        case "Non-Binary Golfers":
                            membershipQuery = membershipQuery.Where(s => s.Gender == "Non-binary");
                            break;
                        case "Other Golfers":
                            membershipQuery = membershipQuery.Where(s => s.Gender == "Other");
                            break;
                        case "Scratch Players":
                            membershipQuery = membershipQuery.Where(s => s.Handicap == 0);
                            break;
                        case "Player Handicap less 10":
                            membershipQuery = membershipQuery.Where(s => s.Handicap > 0 && s.Handicap <= 10);
                            break;
                        case "Player Handicap Greater than 10 and less than 20":
                            membershipQuery = membershipQuery.Where(s => s.Handicap > 10 && s.Handicap < 20);
                            break;
                        case "Player Handicap Greater 20":
                            membershipQuery = membershipQuery.Where(s => s.Handicap > 20);
                            break;
                        default:
                            membershipQuery = membershipQuery.Where(s => s.FirstName == CurrentFilter);
                            break;
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

