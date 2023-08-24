using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_CommerceASP.Data;
using E_CommerceASP.Models.Entities;
using E_CommerceASP.Areas.Admin.Models.ViewModels;

namespace E_CommerceASP.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class TagController : Controller
	{
		private readonly AppDbContext _context;

		public TagController(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _context.Tags.ToListAsync());
		}

		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Tags == null)
				return NotFound();

			var tag = await _context.Tags.FirstOrDefaultAsync(m => m.Id == id);

			if (tag == null)
				return NotFound();

			return View(tag);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateTagViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var tag = new Tag()
				{
					Name = viewModel.Name.ToLower()
				};

				_context.Tags.Add(tag);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}

			return View();
		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Tags == null)
				return NotFound();

			var tag = await _context.Tags.FindAsync(id);

			if (tag == null)
				return NotFound();

			var viewModel = new EditTagViewModel()
			{
				Id = tag.Id,
				Name = tag.Name
			};

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, EditTagViewModel viewModel)
		{
			if (id != viewModel.Id)
				return NotFound();

			if (ModelState.IsValid)
			{
				var tag = new Tag()
				{
					Id = viewModel.Id,
					Name = viewModel.Name.ToLower()
				};

				_context.Tags.Update(tag);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}

			return View(viewModel);
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Tags == null)
				return NotFound();

			var tag = await _context.Tags.FindAsync(id);

			if (tag == null)
				return NotFound();

			_context.Tags.Remove(tag);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}
	}
}
