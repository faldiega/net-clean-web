using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetApp.Application.DTOs;
using NetApp.Application.Interfaces;

namespace NetApp.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    // GET: /Product
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllAsync();
        return View(products);
    }

    // GET: /Product/Create
    public async Task<IActionResult> Create()
    {
        await PopulateCategoriesAsync();
        return View();
    }

    // POST: /Product/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateProductDto dto)
    {
        if (!ModelState.IsValid)
        {
            await PopulateCategoriesAsync();
            return View(dto);
        }

        try
        {
            await _productService.CreateAsync(dto);
            TempData["Success"] = "Product created successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            await PopulateCategoriesAsync();
            return View(dto);
        }
    }

    // GET: /Product/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product is null)
            return NotFound();

        var dto = new UpdateProductDto
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Price = product.Price,
            Stock = product.Stock,
            CategoryId = product.CategoryId
        };

        await PopulateCategoriesAsync(dto.CategoryId);
        return View(dto);
    }

    // POST: /Product/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateProductDto dto)
    {
        if (!ModelState.IsValid)
        {
            await PopulateCategoriesAsync(dto.CategoryId);
            return View(dto);
        }

        try
        {
            await _productService.UpdateAsync(dto);
            TempData["Success"] = "Product updated successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            await PopulateCategoriesAsync(dto.CategoryId);
            return View(dto);
        }
    }

    // GET: /Product/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product is null)
            return NotFound();

        return View(product);
    }

    // POST: /Product/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _productService.DeleteAsync(id);
        TempData["Success"] = "Product deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

    // ── Helper ───────────────────────────────────────────────
    private async Task PopulateCategoriesAsync(int selectedId = 0)
    {
        var categories = await _categoryService.GetAllAsync();
        ViewBag.Categories = new SelectList(categories, "CategoryId", "Name", selectedId);
    }
}