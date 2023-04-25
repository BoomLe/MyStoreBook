using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Book.Models;
using Book.Unitity;
using Book.Areas.Admin.Models;

namespace Book.Areas.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
     private readonly CartService _cartService;
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitityOfWork _unitityOfWork;
    public HomeController(ILogger<HomeController> logger,IUnitityOfWork unitityOfWork ,CartService cartService)
    {
        _logger = logger;
        _unitityOfWork = unitityOfWork;
        _cartService = cartService;
    }

    public IActionResult Index()
    {
        IEnumerable<Products> listProduct = _unitityOfWork.Product.GetAll(includeProperties: "Category");
        return View(listProduct);
    }

    public IActionResult Details(int productId)
    {
        Products product = _unitityOfWork.Product.Get(p=> p.Id == productId, includeProperties: "Category");
        // IEnumerable<Products> listProduct = _unitityOfWork.Product.GetAll(includeProperties: "Category");

        return View(product);

    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

               /// Thêm sản phẩm vào cart
[Route ("addcart/{productid:int}", Name = "addcart")]
public IActionResult AddToCart ([FromRoute] int productid) {

    var product = _unitityOfWork.Product.GetAll()
        .Where (p => p.Id == productid)
        .FirstOrDefault ();
    if (product == null)
        return NotFound ("Không có sản phẩm");

    // Xử lý đưa vào Cart ...
    var cart =_cartService.GetCartItems ();
    var cartitem = cart.Find (p => p.product.Id == productid);
    if (cartitem != null) {
        // Đã tồn tại, tăng thêm 1
        cartitem.quantity++;
    } else {
        //  Thêm mới
        cart.Add (new CartItem () { quantity = 1, product = product });
    }

    // Lưu cart vào Session
    _cartService.SaveCartSession (cart);
    // Chuyển đến trang hiện thị Cart
    return RedirectToAction (nameof (Cart));
    }
    
 // Hiện thị giỏ hàng
    [Route ("/cart", Name = "cart")]
    public IActionResult Cart () 
    {
    return View (_cartService.GetCartItems());
    }  

    /// xóa item trong cart
    [Route ("/removecart/{productid:int}", Name = "removecart")]
    public IActionResult RemoveCart ([FromRoute] int productid) {
    var cart =_cartService.GetCartItems ();
    var cartitem = cart.Find (p => p.product.Id == productid);
    if (cartitem != null) {
        // Đã tồn tại, tăng thêm 1
        cart.Remove(cartitem);
    }

   _cartService.SaveCartSession (cart);
    return RedirectToAction (nameof (Cart));
}           
/// Cập nhật
[Route ("/updatecart", Name = "updatecart")]
[HttpPost]
public IActionResult UpdateCart ([FromForm] int productid, [FromForm] int quantity) {
    // Cập nhật Cart thay đổi số lượng quantity ...
    var cart =_cartService.GetCartItems ();
    var cartitem = cart.Find (p => p.product.Id == productid);
    if (cartitem != null) {
        // Đã tồn tại, tăng thêm 1
        cartitem.quantity = quantity;
    }
   _cartService.SaveCartSession (cart);
    // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
    return Ok();
}
  [Route("/check-out/")]
  public IActionResult Checkout()
  {
    var cart = _cartService.GetCartItems();
    _cartService.ClearCart();

    return View();
  }
}
