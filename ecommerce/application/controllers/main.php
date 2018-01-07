<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class Main extends CI_Controller {

	public function __construct(){
		parent::__construct();
		$this->load->model('product_model','prod');
		$this->load->model('user_model','user');
    }

	//page loader
	public function index(){
		//$featured_product = $this->prod->get_featured_product();
		$categories = $this->prod->get_categories();
		$news = $this->prod->get_news();
		$sidebar = array('login','cart','categories','news');
		$items_cart = $this->get_cart();
		$new_produt = $this->prod->get_new_product();
		$home_content = $this->user->get_welcome();
		$contactus = $this->user->get_contactus();
		$data = array(
			"home_content"=>$home_content,
			"contactus"=>$contactus,
			"categories"=>$categories,
			//"featured_product"=>$featured_product,
			"product"=>$new_produt,
			"items_cart"=>$items_cart,
			"news"=>$news,
			"mainbar"=>"home",
			"title"=>"Home",
			"sidebar"=>$sidebar,
			"js_files"=>array(),
			"css_files"=>array()
		);
		$this->load->view("main/index",$data);
	}
	public function product($subcategory_id=NULL,$key=NULL){
		$product = NULL;
		if($subcategory_id==NULL && $key==NULL){
			$product = $this->prod->get_featured_product();
		}
		$title = "Featured Product";
		if($subcategory_id!=NULL){
			$product = $this->prod->get_product($subcategory_id);
			$title = $this->prod->get_title($subcategory_id);
		}
		if($subcategory_id==NULL && $key!=NULL){
			$product = $this->prod->get_product(NULL,$key);
			$title = $this->prod->get_title(NULL,$key);
		}
		//$new_produt = $this->prod->get_new_product();
		$categories = $this->prod->get_categories();
		$news = $this->prod->get_news();
		$sidebar = array('login','cart','categories','news');
		$items_cart = $this->get_cart();
		$contactus = $this->user->get_contactus();
		$data = array(
			"contactus"=>$contactus,
			"categories"=>$categories,
			"product"=>$product,
			"items_cart"=>$items_cart,
			"news"=>$news,
			"mainbar"=>"product",
			"title"=>$title,
			"sidebar"=>$sidebar,
			"js_files"=>array(),
			"css_files"=>array()
		);
		$this->load->view("main/index",$data);
	}

	public function register($msg=NULL){
		$sidebar = array('login','cart','categories','news');
		$news = $this->prod->get_news();
		$items_cart = $this->get_cart();
		$categories = $this->prod->get_categories();
		$contactus = $this->user->get_contactus();
		$data = array(
			"categories"=>$categories,
			"contactus"=>$contactus,
			"msg"=>$msg,
			"items_cart"=>$items_cart,
			"news"=>$news,
			"mainbar"=>"register",
			"title"=>"Register",
			"sidebar"=>$sidebar,
			"js_files"=>array(),
			"css_files"=>array()
		);
		$this->load->view("main/index",$data);
	}
	public function profile(){

		$sidebar = array('login','cart','categories','news');
		$transactions = $this->get_transactions();
		$user = $this->user->get_userdata($this->session->userdata('user_id'));
		$categories = $this->prod->get_categories();
		$news = $this->prod->get_news();
		$items_cart = $this->get_cart();
		$contactus = $this->user->get_contactus();

		$js_files = array(
			base_url().'assets/grocery_crud/themes/datatables/js/jquery.dataTables.min.js'
		);
		$css_files = array(
			base_url().'assets/grocery_crud/themes/datatables/css/datatables.css',
			base_url().'assets/grocery_crud/themes/datatables/css/jquery.dataTables.css',
			base_url().'assets/grocery_crud/themes/datatables/css/jquery.dataTables_themeroller.css'
		);

		$data = array(
			"categories"=>$categories,
			"contactus"=>$contactus,
			"transactions"=>$transactions,
			"user"=>$user,
			"items_cart"=>$items_cart,
			"news"=>$news,
			"mainbar"=>"profile",
			"title"=>"Profile",
			"sidebar"=>$sidebar,
			'js_files' => $js_files,
			'css_files' => $css_files
		);
		$this->load->view("main/index",$data);
	}

	public function payment_confirmation(){
		$sidebar = array('login','cart','categories','news');
		$transactions = $this->get_transactions();
		$user = $this->user->get_userdata($this->session->userdata('user_id'));
		$categories = $this->prod->get_categories();
		$news = $this->prod->get_news();
		$items_cart = $this->get_cart();
		$contactus = $this->user->get_contactus();

		$js_files = array(
			base_url().'assets/grocery_crud/themes/datatables/js/jquery.dataTables.min.js'
		);
		$css_files = array(
			base_url().'assets/grocery_crud/themes/datatables/css/datatables.css',
			base_url().'assets/grocery_crud/themes/datatables/css/jquery.dataTables.css',
			base_url().'assets/grocery_crud/themes/datatables/css/jquery.dataTables_themeroller.css'
		);

		$data = array(
			"categories"=>$categories,
			"contactus"=>$contactus,
			"transactions"=>$transactions,
			"user"=>$user,
			"items_cart"=>$items_cart,
			"news"=>$news,
			"mainbar"=>"profile/transactions",
			"title"=>"Payment Confirmation",
			"sidebar"=>$sidebar,
			'js_files' => $js_files,
			'css_files' => $css_files
		);
		$this->load->view("main/index",$data);
	}

	public function detail_transaction($cart_id){
		$sidebar = array('login','cart','categories','news');
		//$cart_id=$this->cart->get_cart_id($payment_id);
		$categories = $this->prod->get_categories();
		$news = $this->prod->get_news();
		$items_cart = $this->get_cart();
		$table = $this->get_details($cart_id);
		$contactus = $this->user->get_contactus();

		$js_files = array(
			//base_url().'assets/grocery_crud/themes/datatables/js/jquery.js',
			//base_url().'assets/grocery_crud/themes/datatables/js/jquery-ui-1.8.10.custom.min.js',
			base_url().'assets/grocery_crud/themes/datatables/js/jquery.dataTables.min.js'

		);
		$css_files = array(
			base_url().'assets/grocery_crud/themes/datatables/css/datatables.css',
			base_url().'assets/grocery_crud/themes/datatables/css/jquery.dataTables.css',
			base_url().'assets/grocery_crud/themes/datatables/css/jquery.dataTables_themeroller.css'
		);

		$data = array(
			"table" => $table,
			"categories"=>$categories,
			"contactus"=>$contactus,
			"items_cart"=>$items_cart,
			"news"=>$news,
			"mainbar"=>"detail_transaction",
			"title"=>"Detail Transaction",
			"sidebar"=>$sidebar,
			'js_files' => $js_files,
			'css_files' => $css_files
		);
		$output = array_merge($data);
		$this->load->view("main/index",$data);
	}

	public function detail_product($item_id){
		$item = $this->prod->get_product_row($item_id);
		$pictures = $this->prod->get_pictures($item_id);
		$sidebar = array('login','cart','categories','news');
		$categories = $this->prod->get_categories();
		$news = $this->prod->get_news();
		$items_cart = $this->get_cart();
		$contactus = $this->user->get_contactus();
		$data = array(
			"categories"=>$categories,
			"contactus"=>$contactus,
			"pictures" => $pictures,
			"item" => $item,
			"items_cart"=>$items_cart,
			"news"=>$news,
			"mainbar"=>"detail_product",
			"title"=>"Detail Product",
			"sidebar"=>$sidebar,
			"js_files"=>array(),
			"css_files"=>array()
		);
		$this->load->view("main/index",$data);
	}
	public function how_to_order(){
		
		$sidebar = array('login','cart','categories','news');
		$news = $this->prod->get_news();
		$items_cart = $this->get_cart();
		$categories = $this->prod->get_categories();
		$how_to_order = $this->prod->get_how_to_order();
		$term_of_service = $this->prod->get_term_of_service();
		$contactus = $this->user->get_contactus();
		
		$data = array(
			"categories"=>$categories,
			"contactus"=>$contactus,
			"term_of_service" => $term_of_service,
			"how_to_order"=>$how_to_order,
			"items_cart"=>$items_cart,
			"news"=>$news,
			"mainbar"=>"how_to_order",
			"title"=>"How To Order",
			"sidebar"=>$sidebar,
			"js_files"=>array(),
			"css_files"=>array()
		);
		$this->load->view("main/index",$data);
	}
	public function news($news_id){

		$sidebar = array('login','cart','categories','news');
		$news = $this->prod->get_news();
		$items_cart = $this->get_cart();
		$categories = $this->prod->get_categories();
		$news_detail = $this->prod->get_news($news_id)->first_row();
		$title = $news_detail->title;
		$contactus = $this->user->get_contactus();
		$data = array(
			"news_detail"=>$news_detail,
			"categories"=>$categories,
			"contactus"=>$contactus,
			"items_cart"=>$items_cart,
			"news"=>$news,
			"mainbar"=>"news_detail",
			"title"=>$title,
			"sidebar"=>$sidebar,
			"js_files"=>array(),
			"css_files"=>array()
		);
		$this->load->view("main/index",$data);
	}
	public function contactus(){

		$sidebar = array('login','cart','categories','news');
		$news = $this->prod->get_news();
		$items_cart = $this->get_cart();
		$categories = $this->prod->get_categories();
		$contactus = $this->user->get_contactus();
		
		$data = array(
			"categories"=>$categories,
			"contactus"=>$contactus,
			"items_cart"=>$items_cart,
			"news"=>$news,
			"mainbar"=>"contactus",
			"title"=>"Contact Us",
			"sidebar"=>$sidebar,
			"js_files"=>array(),
			"css_files"=>array()
		);
		$this->load->view("main/index",$data);
	}
	public function mycart(){
		$total = 0;
		$table = "";
		$table.="<table class='cart'>";
		/*$table.="<tr>
					<td class='head'>
						Product Name
					</td>
					<td class='head'>
						Description
					</td>
					<td class='head'>
						Price
					</td>
					<td class='head'>
						Qty
					</td>
					<td class='head'>
						Subtotal
					</td>
				</tr>";*/

		if($this->session->userdata('cart')){
			$cart = $this->session->userdata('cart');

			$subtotal = 0;
			foreach($cart as $row){
				$subtotal = $row['price']*$row['qty'];
				$total += $subtotal;

				$table.="<tr>
							<td>
								".$row['item']." - ".$this->prod->get_color_name($row['color_id'])."
							</td>
							<td align='center'>
								<input value='".$row['qty']."' size='2' />
							</td>
							<td>
								Rp. ".$row['price']."
							</td>
							<td>
								Rp. $subtotal
							</td>
						</tr>";
			}
		}
		if($total>0){
			$table.="<tr>
						<td colspan='2'>
						</td>
						<td>
							Total
						</td>
						<td>
							Rp. $total
						</td>
					</tr>
					<tr>
						<td colspan='3'>
							<div style='float:left;margin:0 30px 0 50px'>Comment</div><textarea id='remark' style='resize:none' cols='40'></textarea>
						</td>
						<td>
							<input type='button' id='checkout' value='Check Out'/>
						</td>
					</tr>";
		}
		else{
			$table.="<tr>
						<td colspan='4' class='nodata'>
							No Data
						</td>
					</tr>";
		}

		$table.="</table>";

		$sidebar = array('login','cart','categories','news');
		$categories = $this->prod->get_categories();
		$news = $this->prod->get_news();
		$items_cart = $this->get_cart();
		$contactus = $this->user->get_contactus();

		$data = array(
			"categories"=>$categories,
			"contactus"=>$contactus,
			"table"=>$table,
			"items_cart"=>$items_cart,
			"news"=>$news,
			"mainbar"=>"mycart",
			"title"=>"My Cart",
			"sidebar"=>$sidebar,
			"js_files"=>array(),
			"css_files"=>array()
		);
		$this->load->view("main/index",$data);
	}
	public function payment($cart_id){

		$payment = $this->getTablePayment($cart_id);

		$sidebar = array('login','cart','categories','news');
		$categories = $this->prod->get_categories();
		$news = $this->prod->get_news();
		$items_cart = $this->get_cart();
		$contactus = $this->user->get_contactus();

		$data = array(
			"categories"=>$categories,
			"contactus"=>$contactus,
			"payment"=>$payment,
			"items_cart"=>$items_cart,
			"err"=>'',
			"news"=>$news,
			"mainbar"=>"payment",
			"title"=>"Payment Confirmation",
			"sidebar"=>$sidebar,
			"js_files"=>array(),
			"css_files"=>array()
		);
		$this->load->view("main/index",$data);
	}

	//process
	public function dologout(){
		$this->session->unset_userdata('user_id');
		$this->session->unset_userdata('username');
		$this->session->unset_userdata('email');
		$this->index();
	}
	
	
	public function dosearch($key){
		$this->product(NULL,$key);
	}
	//ajax process
	public function doregister(){
		$username = $this->input->post("username");
		$password = md5($this->input->post("password"));
		$email = $this->input->post("email");
		$address = $this->input->post("address");
		$city = $this->input->post("city");
		$phone = $this->input->post("phone");
		$gender = $this->input->post("gender");
		$birthday = $this->input->post("birthday");
		header('Content-Type: application/json; charset=UTF-8', true);
		$register = array();
		if($username!="" && $password!="" && $email!=""){
			if($this->user->register($username,$password,$email,$gender,$birthday,$address,$city,$phone)){
				$register['status'] ="Register Successful";
			}
			else{
				$register['status'] = "Register Failed";
			}
		}
		else{
			$register['status'] = "All field must be filled";
		}
		$register=array('register'=>$register);
		echo json_encode($register);
	}
	public function dologin(){
		$username = $this->input->post("username");
		$password = $this->input->post("password");
		header('Content-Type: application/json; charset=UTF-8', true);
		$login = array();
		if($username =="" || $password==""){
			$login['status']="All field must be filled";
		}
		else{
			$user = $this->user->login($username);
			if($user!=FALSE){
				if($user->password == md5($password)){
					$login['status'] = 'Login Successful';
					$login['username'] = $username;
					$this->session->set_userdata('user_id',$user->user_id);
					$this->session->set_userdata('username',$user->username);
					$this->session->set_userdata('email',$user->email);
				}
				else{
					$login['status'] = "Wrong password";
				}
			}
			else{
				$login['status'] = "Username doesn't exists";
			}
		}
		$login = array('login'=>$login);
		echo json_encode($login);
	}
	public function checkout(){
		$this->load->model('cart_model','cart');
		$user_id = $this->session->userdata('user_id');
		$username = $this->session->userdata('username');
		$total = $this->input->post('total');
		$remark = $this->input->post('remark');
		header('Content-Type: application/json; charset=UTF-8', true);
		$cart = $this->session->userdata('cart');
		$query = $this->prod->insert_cart($user_id,date("Y-m-d"),$this->gen_billcode($this->session->userdata('username')),$remark,$total,$cart);
		$status = "";
		$cart_id = "";
		if($query!=false){
			$cart_id = $this->cart->get_last_cart($user_id)->cart_id;
			$status = "success";
			$this->session->unset_userdata('cart');
		}
		else{
			$status = "failure";
		}
		$response = array(
			"status"=>$status,
			"cart_id"=>$cart_id
		);
		echo json_encode($response);
	}
	public function edit_profile(){
		header('Content-Type: application/json; charset=UTF-8', true);
		$msg = "";
		$action = $this->input->post('action');
		$value = $this->input->post('value');
		$status = "";
		if($action=='password'){
			$value = md5($value);
		}
		$result = $this->user->edit_profile($this->session->userdata('user_id'),$action,$value);
		if($result){
			$msg="$action saved";
			$status ="success";
		}
		else{
			$msg="Failed change $action";
			$status = "failed";
		}
		$response = array(
			"status"=>$status,
			"msg"=>$msg
		);
		echo json_encode($response);
	}
	public function contactus_message(){
		header('Content-Type: application/json; charset=UTF-8', true);
		$msg = "";
		$status ="";
		$subject = $this->input->post('subject');
		$email = $this->input->post('email');
		$message = $this->input->post('message');
		$username = 'N/A';
		if($this->session->userdata('username')!=FALSE)
			$username = $this->session->userdata('username');

		$result = $this->user->contactus_message($username,$email,$subject,$message);

		if($result){
			$msg="Message sent";
			$status ="success";
		}
		else{
			$msg="Send message failed";
			$status = "failed";
		}
		$response = array(
			"status"=>$status,
			"msg"=>$msg
		);
		echo json_encode($response);
	}
	
	public function dopayment(){
		header('Content-Type: application/json; charset=UTF-8', true);
		$this->load->model('cart_model','cart');
		
		$cart_id = $this->input->post('cart_id');
		$date = $this->input->post('date');
		$amount_paid = $this->input->post('amount_paid');
		$bank_id = $this->input->post('bank_id');
		$account_name = $this->input->post('account_name');
		$payment_account = $this->input->post('payment_account');
		
		
		$msg = "";
		$status = "";
		if($bank_id != '' && $payment_account!='' && $amount_paid!='' && $date != '' && $account_name!='' && $payment_account!=''){
			$query = $this->cart->dopayment($cart_id,$bank_id,$payment_account,$amount_paid,$date,$account_name);
			if($query){
				$msg="Payment Confirmation Success";
				$status ="success";
			}
			else{
				$msg="Payment Confirmation Failed";
				$status = "failed";
			}

		}
		else{
			$msg="All field must be filled";
			$status = "empty";
		}
		$response = array(
			"status"=>$status,
			"msg"=>$msg
		);
		echo json_encode($response);
	}

	//ajax
	public function add_to_cart(){
		$itemID = $this->input->post("itemID");
		$qty = $this->input->post("qty");
		$size = $this->input->post("size");
		$color = $this->input->post("color");

		$row = $this->prod->get_product_row($itemID);
		$arr_len=0;
		$li = "";
		$idx = 0;
		header('Content-Type: application/json; charset=UTF-8', true);
		if($this->session->userdata('cart')){
			$idx = $this->cek_qty($itemID,$color,$size);

			$cart = $this->session->userdata('cart');
			$arr_len = sizeof($cart);
			if($idx==-1){
				$row_record['item_id'] 	= $itemID;
				$row_record['item'] 	= $row->item;
				$row_record['price'] 	= $row->price;
				$row_record['color_id']	= $color;
				//$row_record['size_id'] 	= $size;
				$row_record['qty'] 		= $qty;
				$row_record['point']	= $row->point;
				$cart[$arr_len] = $row_record;
				$idx = $arr_len;
			}
			else{
				$row_record = $cart[$idx];
				$row_record['qty'] += $qty;
				$qty = $row_record['qty'];
				$cart[$idx] = $row_record;
			}
			$this->session->set_userdata('cart',$cart);
		}
		else{
			$row_record['item_id'] 	= $itemID;
			$row_record['item'] 	= $row->item;
			$row_record['price'] 	= $row->price;
			$row_record['color_id']	= $color;
			//$row_record['size_id'] 	= $size;
			$row_record['qty'] 		= $qty;
			$row_record['point']	= $row->point;

			$cart[0] = $row_record;
			$this->session->set_userdata('cart',$cart);
		}
		$subtotal = $row->price*$qty;
		$table = $row->item." - ".$this->prod->get_color_name($color)." - Rp. ".$row->price." x $qty = Rp. $subtotal";
		$li .= "<li id='cart_".$row->item_id."_$idx' value='".$row->price."'>".$table."<a href='".base_url()."main/delete_from_cart/".$row->item_id."' onClick='return false;'><input type='hidden' id='qty_".$row->item_id."' value='$qty' > <img src='".base_url()."assets/admin/images/icon/forbidden.png' title='Remove' witdh='15' height='15'/></a></li>";
		 $response = array(
		 	"theResponse" => $li,
			"index"=>$idx
		 );
		 echo json_encode($response);
	}
	public function delete_from_cart(){
		$itemID = $this->input->post("itemID");
		$idx = $this->input->post("idx");

		//$idx = $this->cek_qty($itemID,$color,$size);
		$cart = $this->session->userdata("cart");
		unset($cart[$idx]);
		$cart1 = array_values($cart);
		$this->session->set_userdata("cart",$cart1);
	}
	public function get_size($item_id){

		$query = $this->prod->get_size($item_id);

		header('Content-Type: application/json; charset=UTF-8', true);
		$sizes = array();
		foreach($query->result() as $row){
			$sizes[] = array(
				"size_id"	=> $row->size_id,
				"size"		=> $row->size
			);
		}
		$sizes = array(
			"sizes" =>$sizes
		);
		echo json_encode($sizes);
	}
	public function get_color($item_id){

		$query = $this->prod->get_color($item_id);

		header('Content-Type: application/json; charset=UTF-8', true);
		$colors = array();
		foreach($query->result() as $row){
			$colors[] = array(
				"color_id"	=> $row->color_id,
				"color"		=> $row->color
			);
		}
		$colors = array(
			"colors" =>$colors
		);
		echo json_encode($colors);
	}
	public function cek_username($username){
		$availability = array();
		if($this->user->cek_username($username)){
			//not available
			$availability['availability'] = "not available";
		}
		else{
			$availability['availability'] = "available";
		}
		$availability = array('availability'=>$availability);
		echo json_encode($availability);
	}
	
	public function autosearch($key){
		$tags = $this->prod->autosearch($key);
		header('Content-Type: application/json; charset=UTF-8', true);
		$data = array(
			"tags" => $tags
		);
		echo json_encode($data);
	}



	//data request
	public function get_cart(){
		$li = "";
		$total = 0;
		//$this->session->unset_userdata("cart");
		if($this->session->userdata('cart')){
			$cart = $this->session->userdata('cart');

			$subtotal = 0;
			$idx = 0;
			foreach($cart as $row){
				$subtotal = $row['price']*$row['qty'];
				$total += $subtotal;
				$table = $row['item']." - ".$this->prod->get_color_name($row['color_id'])." - Rp. ".$row['price']." x ".$row['qty']." = Rp. $subtotal";
				$li .= "<li id='cart_".$row['item_id']."_$idx' value='".$row['price']."'>".$table."<a href='".base_url()."main/delete_from_cart/".$row['item_id']."' onClick='return false;'><input type='hidden' id='qty_".$row['item_id']."' value='".$row['qty']."' > <img src='".base_url()."assets/admin/images/icon/forbidden.png' title='Remove' witdh='15' height='15'/></a></li>";
				$idx++;
			}

		}
		if($total>0){
			$li .= "<li id='total' value='$total'>Total : Rp. $total</li>";
		}
		else{
			$li .= "<li id='total' value='0'>Now <strong>0 item(s)</strong> in your cart</li>";
		}
		return $li;
	}

	//transactions
	public function get_transactions(){
		$this->load->model("cart_model","cart");
		$transactions = $this->cart->get_carts($this->session->userdata("user_id"));

		$table = '';
		$table.="<table id='transactions'>";
		$table.="
			<thead>
				<tr>
					<th>No</th>
					<th>Bill Code</th>
					<th>Date</th>
					<th>Total Bill</th>
					<th>Status</th>
					<th>Action</th>
				</tr>
			</thead>
			<tbody>";

		if(sizeof($transactions)>0){
			$i=1;
			foreach($transactions as $row){
				$table.="<tr>
							<td>
								$i
							</td>
							<td>
								".$row['bill_code']."
							</td>
							<td>
								".date('d M Y',strtotime($row['date']))."
							</td>
							<td>
								Rp. ".$row['total_bill']."
							</td>
							<td>
								".$row['status']."
							</td>
							<td>
								".$this->get_action($row['status'],$row['cart_id'])."
							</td>
						</tr>";
				$i++;
			}
		}
		else{
			$table.="<tr>
						<td colspan='5' class='nodata'>
							No Transactions
						</td>
					</tr>";
		}

		$table.="</tbody></table>";
		return $table;
	}




	//local functions
	public function cek_qty($id,$color,$size){
		$cart = $this->session->userdata("cart");
		$i = 0;
		foreach($cart as $row){
			if($row['item_id'] == $id && $row['color_id']==$color){
				return $i;
			}
			$i++;
		}
		return -1;
	}
	public function gen_billcode($username){
		$code = md5($username.date('Y-m-d').rand(0,1000));
		$code = mb_substr($code,0,10);
		return $code;
	}
	public function get_action($status,$cart_id){
		$action = "<button class='uniform' value='' onclick='detail(".$cart_id.")'>Detail</button>";
		if($status == "Unpaid"){
			$action .= "<button class='uniform' value='' onclick='payment(".$cart_id.")'>Payment</button>";
		}
		else if($status == "Paid"){
			$action .= "Waiting verification";
		}
		else if($status == "Verified"){
			$action .= "<button class='uniform' onclick='verify(".$cart_id.")' >Ok</button>";
		}
		else if($status == "Rejected"){
			$action .= "<button class='uniform' value='' onclick='payment(".$cart_id.")'>Payment</button>";
		}

		return $action;
	}
	public function getTablePayment($cart_id){
		$this->load->model("cart_model","cart");
		$cart = $this->cart->get_cart_row($cart_id,$this->session->userdata('user_id'));
		$bank = $this->cart->get_bank();
		$table = "<div style='float:right;margin-top:20px'><input type='button' class='uniform' id='back' value='Back' onclick='_back()'></div>
				
				 <div style='border:1px #000 solid;height:auto;margin:20px 0 20px 30px;width:400px;padding:5px 5px 5px 5px' class='round5'>
				 	<div style='font-size:16px;text-align:center'>
						Account Information
						<div class='clear'></div>
					</div>
					<div>
                    	<label class='uniform'>Bank</label>
                        <select class='uniform' onchange='gen_bank()' id='bank'>
							<option value=''>-Bank-</option>";
						foreach($bank->result() as $row){
        		      		$table.= "<option value='".$row->account_number."_".$row->bank_id."'>$row->bank</option>";
						}
        $table .=       "</select>
						<div class='clear'></div>
                    </div>
                    <div>
                    	<label class='uniform'>Bank Account</label>
						<input type='hidden' name='bank_id' id='bank_id' />
                        <label class='uniform' id='bank_account' style='font-weight:bold'></label>
						<div class='clear'></div>
                    </div>
					<div>
                    	<label class='uniform'>Bank Account Name</label>
						<input type='hidden' name='bank_account_name' id='bank_account_name' />
                        <label class='uniform' id='bank_account_name' style='font-weight:bold'></label>
						<div class='clear'></div>
                    </div>
				</div>
				
				<div style='padding-left: 30px;'>
					<div>
						<input type='hidden' name='cart_id' id='cart_id' value='$cart->cart_id' />
						<label class='uniform'>Bill Code</label>
						<label class='uniform' style='font-weight:bold'>$cart->bill_code</label>
						<div class='clear'></div>
					</div>
					<div>
						<label class='uniform'>Total Bill</label>
						<label class='uniform' style='font-weight:bold'>Rp. $cart->total_bill</label>
						<div class='clear'></div>
					</div>
                   
					<div>
						<label class='uniform'>Your Bank Account</label>
						<input type='text' id='payment_account' class='uniform' name='payment_account' placeholder='Account Number'>
						<div class='clear'></div>
					</div>
					<div>
						<label class='uniform'>Your Account Name</label>
						<input type='text' id='account_name' class='uniform' name='account_name' placeholder='Account Name'>
						<div class='clear'></div>
					</div>
					<div>
						<label class='uniform'>Amount of settlement</label>
						<input class='uniform' type='text' id='amount_paid' name='amount_paid' placeholder='Amount Paid'>
						<div class='clear'></div>
					</div>
					<div>
						<label class='uniform'>Date of settlement</label>
						<input class='uniform datepicker' type='text' id='date' name='date' placeholder='Transfer Date'>
						<div class='clear'></div>
					</div>
					<div>
						<label class='uniform'>Additional Information</label>
						<textarea class='uniform datepicker' type='text' id='date' name='date' placeholder='Additional Information' cols='35' rows='3'></textarea>
						<div class='clear'></div>
					</div>
					<div>
						<input type='submit' class='uniform' id='submit' value='Submit'>
					</div>
				</div>";
		return $table;
	}
	public function get_details($cart_id){
		$this->load->model('cart_model','cart');

		$cart = $this->cart->get_cart_row($cart_id);
		$user = $this->user->get_userdata($cart->user_id);

		$table = "";
		$table.= "
					<div style='float:right;margin-top:0px'><a href='".base_url()."main/payment_confirmation'><input type='button' class='uniform' id='back' value='Back'></a></div>
					<div style='font-weight:bold' id='tran_head'>
						<table>
							<tr height='30'>
								<td width='200'>Transaction ID</td>
								<td>$cart->cart_id</td>
							</tr>
							<tr height='30'>
								<td width='200'>Customer Name</td>
								<td>$user->username</td>
							</tr>
							<tr height='30'>
								<td width='200'>Bill Code</td>
								<td>$cart->bill_code</td>
							</tr>
							<tr height='30'>
								<td width='200'>Transaction Date</td>
								<td>$cart->date</td>
							</tr>
							<tr height='30'>
								<td width='200'>Comment</td>
								<td>$cart->remark</td>
							</tr>
							<tr height='30'>
								<td width='200'>Total Bill</td>
								<td>Rp. $cart->total_bill</td>
							</tr>
						</table>
					</div><br>";
		$cart_detail = $this->cart->get_cart_detail($cart_id);
		$table .="

			<table cellpadding='0' cellspacing='0' border='0' class='display' id='dataproduct'>
				<thead>
					<tr>
						<th>Item</th>
						<th>Color</th>
						<th>Price</th>
						<th>Qty</th>
						<th>Subtotal</th>
					</tr>
				</thead>
				<tbody>";
		$nRow=0;
		$subtotal = 0;
		foreach($cart_detail->result() as $row){

			$prod = $this->prod->get_product_row($row->item_id);
			$subtotal = $row->qty*$prod->price;
			$table .=	"<tr class='odd gradeX'>
							<td>$prod->item</td>
							<td>".$this->prod->get_color_name($row->color_id)."</td>
							<td>$prod->price</td>
							<td>$row->qty</td>
							<td>$subtotal</td>
						</tr>";
			$nRow++;
		}
		$table.="</tbody>
			</table>
			";

		return $table;
	}
}