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
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var result = _customerRepository.GetAll();
            return View(result);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _customerRepository == null)
            {
                return NotFound();
            }

            var customer = _customerRepository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CustomerTitle,CustomerName,CustomerSurname,CellPhone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerRepository.Add(customer);
                _customerRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5 //TODO https://dotnettutorials.net/lesson/repository-design-pattern-csharp/
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _customerRepository == null)
            {
                return NotFound();
            }

            var customer = _customerRepository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,CustomerTitle,CustomerName,CustomerSurname,CellPhone")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _customerRepository.Update(customer);
                    _customerRepository.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _customerRepository == null)
            {
                return NotFound();
            }

            var customer = _customerRepository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_customerRepository == null)
            {
                return Problem("Entity set 'SuperStoreContext.Customers'  is null.");
            }
            Customer customer = _customerRepository.GetById(id);
            if (customer != null)
            {
                _customerRepository.Remove(customer);
            }
            _customerRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _customerRepository.GetById(id) != null;
        }
    }
}
