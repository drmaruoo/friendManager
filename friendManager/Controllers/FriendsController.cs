using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using friendManager.Models;

namespace friendManager.Controllers
{
    /// <summary>
    /// This controller handles friends related actions
    /// </summary>
    public class FriendsController : Controller
    {
        private readonly friendManagerContext _context;

        public FriendsController(friendManagerContext context)
        {
            _context = context;
        }

        // GET: Friends
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "date_asc" ? "date_desc" : "date_asc";
            ViewBag.LikeSortParm = sortOrder == "like_asc" ? "like_desc" : "like_asc";
            var friend = from f in _context.Friend
                         select f;
            switch (sortOrder)
            {
                case "name_desc":
                    friend = friend.OrderByDescending(f => f.Name);
                    break;
                case "date_asc":
                    friend = friend.OrderBy(f => f.FirstMeetDate);
                    break;
                case "date_desc":
                    friend = friend.OrderByDescending(f => f.FirstMeetDate);
                    break;
                case "like_asc":
                    friend = friend.OrderBy(f => f.Likeability);
                    break;
                case "like_desc":
                    friend = friend.OrderByDescending(f => f.Likeability);
                    break;
                default:
                    friend = friend.OrderBy(f => f.Name);
                    break;
            }
            return View(await friend.ToListAsync());
        }

        // GET: Friends/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Friend
                .FirstOrDefaultAsync(m => m.ID == id);



            if (friend == null)
            {
                return NotFound();
            }

            return View(friend);
        }

        // GET: Friends/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Friends/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,FirstMeetDate,BiggestSecret,Likeability")] Friend friend)
        {
            if (ModelState.IsValid)
            {
                _context.Add(friend);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(friend);
        }

        // GET: Friends/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Friend.FindAsync(id);
            if (friend == null)
            {
                return NotFound();
            }
            return View(friend);
        }

        // POST: Friends/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,FirstMeetDate,BiggestSecret,Likeability")] Friend friend)
        {
            if (id != friend.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(friend);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FriendExists(friend.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(friend);
        }

        // GET: Friends/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Friend
                .FirstOrDefaultAsync(m => m.ID == id);
            if (friend == null)
            {
                return NotFound();
            }

            return View(friend);
        }

        // POST: Friends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var friend = await _context.Friend.FindAsync(id);
            _context.Friend.Remove(friend);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FriendExists(int id)
        {
            return _context.Friend.Any(e => e.ID == id);
        }
    }
}
