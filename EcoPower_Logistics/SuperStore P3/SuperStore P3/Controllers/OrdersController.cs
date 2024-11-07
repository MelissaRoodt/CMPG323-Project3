using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Data;
using EcoPower_Logistics.Repository;

namespace Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository context)
        {
            _orderRepository = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var superStoreContext = _orderRepository.Include(o => o.Customer);
            return View(await superStoreContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _orderRepository == null)
            {
                return NotFound();
            }

            var order = await _orderRepository.Include(o => o.Customer).FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_orderRepository.Includes(o =>o.Customer), "CustomerId", "CustomerId");
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,OrderDate,CustomerId,DeliveryAddress")] Order order)
        {
            if (ModelState.IsValid)
            {
                _orderRepository.Add(order);
                _orderRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_orderRepository.Includes(o => o.Customer), "CustomerId", "CustomerId", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _orderRepository == null)
            {
                return NotFound();
            }

            var order = _orderRepository.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_orderRepository.Includes(o => o.Customer), "CustomerId", "CustomerId", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderDate,CustomerId,DeliveryAddress")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _orderRepository.Update(order);
                    _orderRepository.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            ViewData["CustomerId"] = new SelectList(_orderRepository.Includes(o => o.Customer), "CustomerId", "CustomerId", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _orderRepository == null)
            {
                return NotFound();
            }

            var order = await _orderRepository
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_orderRepository == null)
            {
                return Problem("Entity set 'SuperStoreContext.Orders'  is null.");
            }
            var order = _orderRepository.GetById(id);
            if (order != null)
            {
                _orderRepository.Remove(order);
            }

            _orderRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _orderRepository.GetById(id) != null;
        }
    }
}
