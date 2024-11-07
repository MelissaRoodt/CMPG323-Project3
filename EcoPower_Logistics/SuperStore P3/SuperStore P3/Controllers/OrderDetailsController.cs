using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using EcoPower_Logistics.Repository;

namespace Controllers
{
    [Authorize]
    public class OrderDetailsController : Controller
    {
        private readonly IOrderDetailsRepository _orderDetailsRepository;

        public OrderDetailsController(IOrderDetailsRepository context)
        {
            _orderDetailsRepository = context;
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            var superStoreContext = _orderDetailsRepository.Include(o => o.Order).Include(o => o.Product);
            return View(await superStoreContext.ToListAsync());
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _orderDetailsRepository == null)
            {
                return NotFound();
            }

            var orderDetail = await _orderDetailsRepository
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderDetailsId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_orderDetailsRepository.Include(o => o.Order), "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(_orderDetailsRepository.Include(o => o.Product), "ProductId", "ProductId");
            return View();
        }

        // POST: OrderDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderDetailsId,OrderId,ProductId,Quantity,Discount")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                _orderDetailsRepository.Add(orderDetail);
                _orderDetailsRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_orderDetailsRepository.Include(o => o.Order), "OrderId", "OrderId", orderDetail.OrderId);
            ViewData["ProductId"] = new SelectList(_orderDetailsRepository.Include(o => o.Product), "ProductId", "ProductId", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _orderDetailsRepository == null)
            {
                return NotFound();
            }

            var orderDetail = _orderDetailsRepository.GetById(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_orderDetailsRepository.Include(o => o.Order), "OrderId", "OrderId", orderDetail.OrderId);
            ViewData["ProductId"] = new SelectList(_orderDetailsRepository.Include(o => o.Product), "ProductId", "ProductId", orderDetail.ProductId);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderDetailsId,OrderId,ProductId,Quantity,Discount")] OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _orderDetailsRepository.Update(orderDetail);
                    _orderDetailsRepository.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(orderDetail.OrderDetailsId))
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
            ViewData["OrderId"] = new SelectList(_orderDetailsRepository.Include(o => o.Order), "OrderId", "OrderId", orderDetail.OrderId);
            ViewData["ProductId"] = new SelectList(_orderDetailsRepository.Include(o => o.Product), "ProductId", "ProductId", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _orderDetailsRepository == null)
            {
                return NotFound();
            }

            var orderDetail = await _orderDetailsRepository
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderDetailsId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_orderDetailsRepository == null)
            {
                return Problem("Entity set 'SuperStoreContext.OrderDetails'  is null.");
            }
            var orderDetail = _orderDetailsRepository.GetById(id);
            if (orderDetail != null)
            {
                _orderDetailsRepository.Remove(orderDetail);
            }

            _orderDetailsRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailExists(int id)
        {
            return _orderDetailsRepository.GetById(id) != null;
        }
    }
}
