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
using E_CommerceASP.Helpers;

namespace E_CommerceASP.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CategoryController : Controller
	{
		private readonly AppDbContext _context;

		public CategoryController(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _context.Categories.ToListAsync());
		}

		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Categories == null)
				return NotFound();

			var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

			if (category == null)
				return NotFound();

			return View(category);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateCategoryViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var category = new Category()
				{
					Name = CapitalizeHelper.Capitalize(viewModel.Name)
				};

				_context.Categories.Add(category);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}

			return View();
		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Categories == null)
				return NotFound();

			var category = await _context.Categories.FindAsync(id);

			if (category == null)
				return NotFound();

			var viewModel = new EditCategoryViewModel()
			{
				Id = category.Id,
				Name = category.Name
			};

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, EditCategoryViewModel viewModel)
		{
			if (id != viewModel.Id)
				return NotFound();

			if (ModelState.IsValid)
			{
				var category = new Category()
				{
					Id = viewModel.Id,
					Name = CapitalizeHelper.Capitalize(viewModel.Name)
				};

				_context.Categories.Update(category);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}

			return View(viewModel);
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Categories == null)
				return NotFound();

			var category = await _context.Categories.FindAsync(id);

			if (category == null)
				return NotFound();

			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}
	}
}
