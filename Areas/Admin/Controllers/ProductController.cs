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
using ECommerce.Helpers;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using E_CommerceASP.Helpers;

namespace E_CommerceASP.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly AppDbContext _context;

		public ProductController(AppDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var products = new List<ProductViewModel>();

			foreach (var product in _context.Products.Include("Category"))
			{
				var viewModel = new ProductViewModel()
				{
					Id = product.Id,
					Name = product.Name,
					Description = product.Description,
					CategoryName = product.Category.Name,
					Price = product.Price,
					ImageUrl = product.ImageUrl
				};

				products.Add(viewModel);
			}

			return View(products);
		}

		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Products == null)
				return NotFound();

			var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

			if (product == null)
				return NotFound();

			var list = _context.ProductTags.Include(pt => pt.Product).Where(pt => pt.ProductId == id).Select(pt => pt.TagId).ToList();

			var tagNames = new List<string>();
			foreach (var item in list)
			{
				tagNames.Add(await _context.Tags.Where(t => t.Id == item).Select(t => t.Name).FirstOrDefaultAsync());
			}

			ViewBag.TagNames = tagNames;

			return View(product);
		}

		public IActionResult Create()
		{
			ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
			ViewBag.Tags = new MultiSelectList(_context.Tags, "Id", "Name");

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateProductViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var product = new Product()
				{
					Name = CapitalizeHelper.Capitalize(viewModel.Name),
					Description = viewModel.Description,
					CategoryId = viewModel.CategoryId,
					Price = viewModel.Price,
					ImageUrl = await UploadFileHelper.UploadFile(viewModel.ImageUrl)
				};

				_context.Products.Add(product);
				await _context.SaveChangesAsync();

				foreach (var tagId in viewModel.TagIds)
				{
					_context.ProductTags.Add(new ProductTag { ProductId = product.Id, TagId = tagId });
				}
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}

			ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
			ViewBag.Tags = new MultiSelectList(_context.Tags, "Id", "Name");

			return View();
		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Products == null)
				return NotFound();

			var product = await _context.Products.Include("Category").FirstOrDefaultAsync(m => m.Id == id);

			if (product == null)
				return NotFound();

			using var stream = new MemoryStream(System.IO.File.ReadAllBytes(product.ImageUrl).ToArray());

			var viewModel = new EditProductViewModel()
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				CategoryId = product.CategoryId,
				Price = product.Price,
				ImageUrl = new FormFile(stream, 0, stream.Length, "ImageUrl", product.ImageUrl.Split(@"\").Last()),
				TagIds = _context.ProductTags.Include(pt => pt.Product).Where(pt => pt.ProductId == product.Id).Select(pt => pt.TagId)
			};

			ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
			ViewBag.Tags = new MultiSelectList(_context.Tags, "Id", "Name");
			ViewBag.Image = product.ImageUrl.Split(@"\").Last();

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, EditProductViewModel viewModel)
		{
			if (id != viewModel.Id)
				return NotFound();

			if (ModelState.IsValid)
			{
				var product = new Product()
				{
					Id = viewModel.Id,
					Name = CapitalizeHelper.Capitalize(viewModel.Name),
					Description = viewModel.Description,
					CategoryId = viewModel.CategoryId,
					Price = viewModel.Price,
					ImageUrl = await UploadFileHelper.UploadFile(viewModel.ImageUrl)
				};

				_context.Products.Update(product);
				await _context.SaveChangesAsync();

				var list = _context.ProductTags.Include(pt => pt.Product).Where(pt => pt.ProductId == product.Id).Select(pt => pt.TagId).ToList();

				foreach (var item in list)
				{
					var pt = new ProductTag { ProductId = product.Id, TagId = item };
					_context.ProductTags.Remove(pt);
				}
				await _context.SaveChangesAsync();

				foreach (var tagId in viewModel.TagIds)
				{
					_context.ProductTags.Add(new ProductTag { ProductId = product.Id, TagId = tagId });
				}
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}

			ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
			ViewBag.Tags = new MultiSelectList(_context.Tags, "Id", "Name");

			return View(viewModel);
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Products == null)
				return NotFound();

			var product = await _context.Products.FindAsync(id);

			if (product == null)
				return NotFound();

			_context.Products.Remove(product);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}
	}
}
