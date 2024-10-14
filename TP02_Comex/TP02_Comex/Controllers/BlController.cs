using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TP02_Comex.Models; // Namespace onde sua classe Bl está
public class BLController : Controller
{
    private readonly BLService _blService;

    public BLController(BLService blService)
    {
        _blService = blService;
    }

    public IActionResult Index()
    {
        var bls = _blService.ObterTodos();
        return View(bls);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Bl bl)
    {
        if (ModelState.IsValid)
        {
            _blService.AdicionarBL(bl); // Verifique se a assinatura está correta
            return RedirectToAction("Index");
        }
        return View(bl);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var bl = _blService.ObterPorId(id);
        if (bl == null)
        {
            return NotFound();
        }
        return View(bl);
    }

    [HttpPost]
    public IActionResult Edit(Bl bl)
    {
        if (ModelState.IsValid)
        {
            _blService.AtualizarBL(bl); // Implemente este método
            return RedirectToAction("Index");
        }
        return View(bl);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        _blService.ExcluirBL(id); // Implemente este método
        return RedirectToAction("Index");
    }
}
