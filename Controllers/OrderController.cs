using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tienda_CURD.Data;
using Tienda_CURD.Models;

namespace Tienda_CURD.Controllers
{
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context = null!;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult>Index(){
            var orders = await _context.Orders
            .Include(p=>p.Client)
            .ToListAsync();

            return View(orders);
        }
        public async Task<ActionResult>Create(){
            ViewBag.Client = await _context.Clientes.ToListAsync();
            ViewBag.Product = await _context.Product.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
         public async Task<ActionResult>Create(Order order, int[] productsIds, int[] cants)
        {
            
            var cli = await _context.Clientes.FirstOrDefaultAsync(c=>c.ClientID == order.ClientID);
            if(cli != null){
                order.Client = cli;
            }

            foreach (var item in productsIds)
            {
                var product = await _context.Product.FindAsync(item);
                if(product != null)
                {
                    order.Details.Add(new OrderDetails{
                        OrderID = item,
                        Cant = cants[Array.IndexOf(productsIds, item)],
                        Product = product 

                    });
                }
            }
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Details(int id)
        {
            var order = await _context.Orders
            .Include(o => o.Details).ThenInclude(d => d.Product)
            .Include(o => o.Client)
            .FirstOrDefaultAsync(o => o.OrderID == id);

            if(order == null)
            {
                return NotFound();
            }
            return View(order); 
        }

        public async Task<IActionResult> Edit(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return NotFound();

            ViewBag.Clients = _context.Clientes.ToList();
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order order)
        {

            if (id != order.OrderID)
                return NotFound();

            _context.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

         public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders
            .Include(o => o.Client)
            .FirstOrDefaultAsync(o => o.OrderID == id);

            if (order == null)
                return NotFound();

            var orderDetails = await _context.OrderDetails
            .Include(o => o.Product)
            .Where(p => p.OrderID == id)
            .ToListAsync();


            ViewBag.oDetails = orderDetails;
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order =  await _context.Orders.FindAsync(id);

            if(order == null)
                return NotFound();

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
    
    
}