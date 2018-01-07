<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class Admin extends CI_Controller {

	public function __construct(){
		parent::__construct();
		if($this->session->userdata('username')==NULL){
			redirect('pagenotfound', 'location', 404);
		}
		$this->load->helper('url');
		$this->load->database();
		$this->load->library('image_CRUD');

    }

	public function output($output = null){
		$this->load->view("admin/index",$output);
	}

	public function index(){
			redirect('admin/product');
	}

	//main menu
	public function mainmenu($menu){
		if($menu == 1){
			redirect('admin/product');
		}
		else if($menu == 2){
			redirect('admin/news');
		}
		else if($menu == 3){
			redirect('admin/transactions');
		}
	}
	//table master
	public function product(){
		try{
			$crud = new grocery_CRUD();
			$this->load->config('grocery_crud');
    		$this->config->set_item('grocery_crud_file_upload_allow_file_types','gif|jpeg|jpg|png');
			$crud->set_theme('flexigrid');
			$crud->set_table('item');
			$crud->set_subject('product');

			$crud->set_field_upload('picture','assets/uploads/images');
			$crud->callback_after_upload(array($this,'callback_after_upload'));
			$crud->required_fields('item','price');

			$crud->set_relation_n_n('colors','tr_color','color','item_id','color_id','color','remark');
			//$crud->set_relation_n_n('sizes','tr_size','size','item_id','size_id','size','remark');

			$crud->columns('item','price','point','subcategory_id','date','total_stock','colors');


			$crud->change_field_type('description','text');

			$crud->add_action('Pictures', base_url().'assets/admin/images/icon/photo-album-icon.png', 'admin/pictures','');
			$crud->add_action('Stock by Color', base_url().'assets/admin/images/icon/edit.png', 'admin/color_stock','');
			//$crud->add_action('Stock by Size', 'assets/admin/images/icon/edit.png', 'admin/size_stock','');

			$crud->unset_add_fields('product_id');
			$crud->set_relation('subcategory_id','subcategory','subcategory');
			$crud->display_as('subcategory_id','Sub Category');
			$output = $crud->render();
			$data = array(
				"page_title"=>"Table Product",
				"main_menu"=>"mainmenu",
				"sidebar"=>"sidebar",
				"page_name"=>"crud",
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function product_stock(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('datatables');
			$crud->set_table('item');
			$crud->set_subject('Product Qty');

			$crud->unset_add();
			$crud->unset_delete();
			$crud->unset_edit();

			$output = $crud->render();
			$data = array(
				"page_title"=>"Table Product",
				"main_menu"=>"mainmenu",
				"sidebar"=>"sidebar",
				"page_name"=>"crud",
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function update_stock(){
		$this->load->model('product_model','prod');
		$id = $this->input->post('id_tochange');
		$stock = $this->input->post('stock');
		$tablename = $this->input->post('tablename');
		$value = "";
		if($this->prod->update_stock($tablename,$id,$stock)){
			$value = "Success";
		}
		else{
			$value = "Failed";
		}
		echo $value;
	}
	public function get_tables($item_id,$tablename){

		$this->load->model('product_model','prod');

		$result = $this->prod->get_table($item_id,$tablename);
		$item = $this->prod->get_product_row($item_id);
		$total=0;
		$table ="

			<table cellpadding='0' cellspacing='0' border='0' class='display' id='dataproduct'>
				<thead>
					<tr>
						<th>Item</th>
						<th id='tablename'>$tablename</th>
						<th>Stock by $tablename</th>
						<th>Edit</th>
					</tr>
				</thead>
				<tbody>";
		$nRow=0;
		foreach($result->result() as $row){
			$total += $row->stock;
			$table .=	"<tr class='odd gradeX'>
							<td>$row->item</td>";
			if($tablename=='size'){
				$table .=	"<td>$row->size</td>";
			}
			else if($tablename=='color'){
				$table .=	"<td>$row->color</td>";
			}
			$table .=		"<td id='stock_$nRow'>$row->stock</td>
							<td><a class='edit' href='#' id='link_".$nRow."_$row->id'><img src='".base_url()."assets/admin/images/icon/edit.png' title='Edit' witdh='15' height='15'/></a></td>
						</tr>";
			$nRow++;
		}
		$table.="</tbody>
			</table>
			";
		$table.= "<br><br><div style='font-weight:bold'><table><tr>
						<td width='200'>Unspecified stock by $tablename</strong></td>
						<td id='unspecified'>".($item->total_stock-$total)."</td>
					</tr>
					<tr><td>&nbsp;</td><td>&nbsp;</td></tr>
					<tr>
						<td width='200'>Total Stock of $item->item</td>
						<td id='total'>$item->total_stock</td>
					</tr></table></div>";
		return $table;
	}
	public function color_stock($item_id){
		try{

			$table = $this->get_tables($item_id,'color');
			$js_files = array(
				base_url().'assets/grocery_crud/themes/datatables/js/jquery.js',
				base_url().'assets/grocery_crud/themes/datatables/js/jquery-ui-1.8.10.custom.min.js',
				base_url().'assets/grocery_crud/themes/datatables/js/jquery.dataTables.min.js',
				base_url().'assets/admin/js/ui.core.js',
				base_url().'assets/admin/js/ui.spinner.js'
			);
			$css_files = array(
				base_url().'assets/grocery_crud/themes/datatables/css/datatables.css',
				base_url().'assets/grocery_crud/themes/datatables/css/jquery.dataTables.css',
				base_url().'assets/grocery_crud/themes/datatables/css/jquery.dataTables_themeroller.css'
			);

			$data = array(
				"table" => $table,
				"page_title"=>"Table Product Color Stock",
				"main_menu"=>"mainmenu",
				"sidebar"=>"sidebar",
				"page_name"=>"datatables",
				'output' => '' ,
				'js_files' => $js_files,
				'css_files' => $css_files
			);
			$output = array_merge($data);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function size_stock($item_id){
		try{
			$table = $this->get_tables($item_id,'size');
			$js_files = array(
				base_url().'assets/grocery_crud/themes/datatables/js/jquery.js',
				base_url().'assets/grocery_crud/themes/datatables/js/jquery-ui-1.8.10.custom.min.js',
				base_url().'assets/grocery_crud/themes/datatables/js/jquery.dataTables.min.js',
				base_url().'assets/admin/js/ui.core.js',
				base_url().'assets/admin/js/ui.spinner.js'
			);
			$css_files = array(
				base_url().'assets/grocery_crud/themes/datatables/css/datatables.css',
				base_url().'assets/grocery_crud/themes/datatables/css/jquery.dataTables.css',
				base_url().'assets/grocery_crud/themes/datatables/css/jquery.dataTables_themeroller.css'
			);
			$data = array(
				"table"=> $table,
				"page_title"=>"Table Product Size Stock",
				"main_menu"=>"mainmenu",
				"sidebar"=>"sidebar",
				"page_name"=>"datatables",
				'output' => '' ,
				'js_files' => $js_files,
				'css_files' => $css_files
			);
			$output = array_merge($data);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function pictures($item_id){
		try{
			$crud = new image_CRUD();
			//$crud->unset_upload();
			//$crud->unset_delete();
			$crud->set_primary_key_field('picture_id');
			$crud->set_url_field('picture');
			$crud->set_table('pictures')
			->set_title_field('title')
			->set_ordering_field('status')
			->set_relation_field('item_id')
			->set_image_path('assets/uploads/images');
			$output = $crud->render();

			$data = array(
				"page_title"=>"Pictures",
				"main_menu"=>"mainmenu",
				"sidebar"=>"sidebar",
				"page_name"=>"crud"
				//'output' => '' , 'js_files' => array() , 'css_files' => array()
			);
			//$arr = array('js_files' => array() , 'css_files' => array());
			//$this->load->view("admin/index",$output);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}

	function callback_after_upload($uploader_response,$field_info, $files_to_upload){
		$this->load->library('image_moo');

		//Is only one file uploaded so it ok to use it with $uploader_response[0].
		$file_uploaded = $field_info->upload_path.'/'.$uploader_response[0]->name;

		$this->image_moo->load($file_uploaded)->resize(800,600)->save($file_uploaded,true);

		return true;
	}

	public function size(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('flexigrid');
			$crud->set_table('size');
			$crud->set_subject('Product Size');
			$crud->required_fields('size');
			$crud->columns('size_id','size');
			$crud->unset_add_fields('size_id');
			//$crud->set_relation('album_id','album','album');
			$output = $crud->render();
			$data = array(
				"page_title"=>"Table Product Size",
				"main_menu"=>"mainmenu",
				"sidebar"=>"sidebar",
				"page_name"=>"crud",
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function color(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('flexigrid');
			$crud->set_table('color');
			$crud->set_subject('Product Color');
			$crud->required_fields('color');
			$crud->columns('color_id','color');
			$crud->unset_add_fields('size_id');
			//$crud->set_relation('album_id','album','album');
			$output = $crud->render();
			$data = array(
				"page_title"=>"Table Product Color",
				"main_menu"=>"mainmenu",
				"sidebar"=>"sidebar",
				"page_name"=>"crud",
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function category(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('flexigrid');
			$crud->set_table('category');
			$crud->set_subject('Category Product');
			$crud->required_fields('category');
			$crud->columns('category_id','category');
			$crud->unset_add_fields('category_id');
			//$crud->set_relation('album_id','album','album');
			$output = $crud->render();
			$data = array(
				"page_title"=>"Table Category",
				"main_menu"=>"mainmenu",
				"sidebar"=>"sidebar",
				"page_name"=>"crud",
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function subcategory(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('flexigrid');
			$crud->set_table('subcategory');
			$crud->set_subject('Sub Category Product');
			$crud->required_fields('subcategory','category_id');
			$crud->columns('subcategory_id','subcategory','category_id');
			$crud->unset_add_fields('subcategory_id');
			$crud->set_relation('category_id','category','category');
			$output = $crud->render();
			$data = array(
				"page_title"=>"Table Sub Category",
				"main_menu"=>"mainmenu",
				"sidebar"=>"sidebar",
				"page_name"=>"crud",
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function bank(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('flexigrid');
			$crud->set_table('bank');
			$crud->set_subject('bank');
			$crud->required_fields('bank','account_number');
			$crud->columns('bank','account_number');

			$crud->unset_add_fields('bank_id');

			$output = $crud->render();
			$data = array(
				"page_title"=>"Table Bank",
				"main_menu"=>"mainmenu",
				"sidebar"=>"sidebar",
				"page_name"=>"crud",
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function users(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('flexigrid');
			$crud->set_table('user');
			$crud->set_subject('Users');
			//$crud->required_fields('bank','account_number');
			$crud->change_field_type('user_id','readonly');
			$crud->change_field_type('username','readonly');
			$crud->change_field_type('email','readonly');
			$crud->change_field_type('birthday','readonly');
			$crud->change_field_type('gender','readonly');
			$crud->change_field_type('address','readonly');
			$crud->change_field_type('city','readonly');
			$crud->change_field_type('phone','readonly');
			
			$crud->columns('user_id','username','email','birthday','gender','address','city','phone');
			$crud->unset_add();
			$crud->unset_edit_fields('password','user_type_id','total_point');

			$output = $crud->render();
			$data = array(
				"page_title"=>"Users",
				"main_menu"=>"mainmenu",
				"sidebar"=>"settings",
				"page_name"=>"crud",
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}


	//settings
	public function welcome(){
		try{
			$crud = new grocery_CRUD();
			$this->load->config('grocery_crud');
			$crud->set_theme('flexigrid');
			$crud->set_table('welcome_page');
			$crud->set_subject('Welcome Page Setting');
			
			$crud->columns('content');

			$output = $crud->render();
			$data = array(
				"page_title"=>"Welcome Page Setting",
				"main_menu"=>"mainmenu",
				"sidebar"=>"settings",
				"page_name"=>"crud",
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function message(){
		try{
			$crud = new grocery_CRUD();
			$this->load->config('grocery_crud');
			$crud->set_theme('flexigrid');
			$crud->set_table('message');
			$crud->set_subject('Contact Us Message');
			
			$crud->columns('subject','username','email','message');
			
			$crud->change_field_type('username','readonly');
			$crud->change_field_type('email','readonly');
			$crud->change_field_type('subject','readonly');
			$crud->change_field_type('message','readonly');
			$crud->unset_add();
			$crud->unset_delete();

			$output = $crud->render();
			$data = array(
				"page_title"=>"Contact Us Message",
				"main_menu"=>"mainmenu",
				"sidebar"=>"settings",
				"page_name"=>"crud",
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function news(){
		try{
			$crud = new grocery_CRUD();
			$this->load->config('grocery_crud');
    		$this->config->set_item('grocery_crud_file_upload_allow_file_types','gif|jpeg|jpg|png');
			$crud->set_theme('flexigrid');
			$crud->set_table('news');
			$crud->set_subject('news');
			$crud->set_field_upload('picture','assets/uploads/news_images');
			$crud->callback_after_upload(array($this,'callback_after_upload'));
			$crud->required_fields('title','news','status');
			$crud->columns('title','picture','news','date','status');

			$crud->unset_add_fields('news_id');
			//$crud->set_relation('album_id','album','album');
			$output = $crud->render();
			$data = array(
				"page_title"=>"Setting News",
				"main_menu"=>"mainmenu",
				"sidebar"=>"settings",
				"page_name"=>"crud",
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}

	public function contactus(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('flexigrid');
			$crud->set_table('contactus');
			$crud->set_subject('Contact Us');
			$crud->required_fields('type','contact');
			$output = $crud->render();
			$data = array(
				"page_title"=>"Contact Us Setting",
				"main_menu"=>"mainmenu",
				"sidebar"=>"settings",
				"page_name"=>"crud",
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function new_arrival(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('flexigrid');
			$crud->set_table('new_arrival');
			$crud->set_subject('New Arrival');
			$crud->required_fields('item_id');
			$crud->set_relation('item_id','item','{item_id} - {item}');
			$crud->display_as('item_id','Product ID - Product');
			$output = $crud->render();
			$data = array(
				"page_title"=>"New Arrival Setting",
				"main_menu"=>"mainmenu",
				"sidebar"=>"settings",
				"page_name"=>"crud",
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function featured(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('flexigrid');
			$crud->set_table('featured_product');
			$crud->set_subject('Featured Product');
			$crud->required_fields('item_id');
			$crud->set_relation('item_id','item','{item_id} - {item}');
			$crud->display_as('item_id','Product ID - Product');
			$output = $crud->render();
			$data = array(
				"page_title"=>"Featured Product Setting",
				"main_menu"=>"mainmenu",
				"sidebar"=>"settings",
				"page_name"=>"crud",
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function how_to_order(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('flexigrid');
			$crud->set_table('how_to_order');
			$crud->set_subject('How To Order');
			$crud->required_fields('content');
			
			$output = $crud->render();
			$data = array(
				"page_title"=>"How To Order Setting",
				"main_menu"=>"mainmenu",
				"sidebar"=>"settings",
				"page_name"=>"crud",
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function term_of_service(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('flexigrid');
			$crud->set_table('term_of_service');
			$crud->set_subject('Term Of Service');
			$crud->required_fields('content');
			
			$output = $crud->render();
			$data = array(
				"page_title"=>"Term Of Service Setting",
				"main_menu"=>"mainmenu",
				"sidebar"=>"settings",
				"page_name"=>"crud",
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}

	//transactions
	public function transactions(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('datatables');
			$crud->set_table('payment');
			$crud->where('payment_status','Paid');
			$crud->set_subject('Payment Verification');
			$crud->set_relation('cart_id','cart','{user_id} - {bill_code} - Rp. {total_bill}');
			$crud->display_as('cart_id','User ID - Bill Code - Total Bill');
			$crud->add_action('Detail', '', 'admin/detail','ui-icon-view');
			$crud->add_action('Verify', '', 'admin/verify','ui-icon-view');
			$crud->add_action('Reject', '', 'admin/reject','ui-icon-view');
			//$crud->required_fields('content');
			//$crud->columns('content');
			$crud->unset_add();
			$crud->unset_delete();
			$crud->unset_edit();
			$output = $crud->render();
			$data = array(
				"page_title"=>"Payment Verification",
				"main_menu"=>"mainmenu",
				"sidebar"=>"transactions",
				"page_name"=>"crud",
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function shipment($payment_id=NULL){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('datatables');
			$crud->set_table('payment');
			$crud->where('payment_status','Verified');
			if($payment_id!=NULL){
				$crud->where('payment_id',$payment_id);
			}
			$crud->set_subject('Verified Payment');
			$crud->add_action('Detail', '', 'admin/detail','ui-icon-view');
			$crud->add_action('Send', '', 'admin/send','ui-icon-view');
			$crud->unset_add();
			$crud->unset_delete();
			$crud->unset_edit();
			$output = $crud->render();
			$data = array(
				"page_title"=>"Verified Payment",
				"main_menu"=>"mainmenu",
				"sidebar"=>"transactions",
				"page_name"=>"crud",
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function history($payment_id=NULL){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('datatables');
			$crud->set_table('payment');
			if($payment_id!=NULL){
				$crud->where('payment_id',$payment_id);
			}
			$crud->set_subject('Transactions History');
			$crud->add_action('Detail', '', 'admin/detail','ui-icon-view');
			$crud->unset_add();
			$crud->unset_delete();
			$crud->unset_edit();
			$output = $crud->render();
			$data = array(
				"page_title"=>"Transactions History",
				"main_menu"=>"mainmenu",
				"sidebar"=>"transactions",
				"page_name"=>"crud",
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function detail($payment_id){
		$this->load->model('cart_model','cart');
		$cart_id=$this->cart->get_cart_id($payment_id);

		$table = $this->get_details($cart_id);

		$js_files = array(
			base_url().'assets/grocery_crud/themes/datatables/js/jquery.js',
			base_url().'assets/grocery_crud/themes/datatables/js/jquery-ui-1.8.10.custom.min.js',
			base_url().'assets/grocery_crud/themes/datatables/js/jquery.dataTables.min.js'

		);
		$css_files = array(
			base_url().'assets/grocery_crud/themes/datatables/css/datatables.css',
			base_url().'assets/grocery_crud/themes/datatables/css/jquery.dataTables.css',
			base_url().'assets/grocery_crud/themes/datatables/css/jquery.dataTables_themeroller.css'
		);

		$data = array(
			"table" => $table,
			"page_title"=>"Detail Transaction",
			"main_menu"=>"mainmenu",
			"sidebar"=>"transactions",
			"page_name"=>"datatables",
			'output' => '' ,
			'js_files' => $js_files,
			'css_files' => $css_files
		);
		$output = array_merge($data);
		$this->output($output);
	}
	public function verify($payment_id){
		$this->load->model('cart_model','cart');
		$this->cart->verify($payment_id,'Verified');
		redirect('admin/shipment/'.$payment_id);
	}
	public function reject($payment_id){
		$this->load->model('cart_model','cart');
		$this->cart->verify($payment_id,'Rejected');
		redirect('admin/history/'.$payment_id);
	}
	public function send($payment_id){
		$this->load->model('cart_model','cart');
		$this->cart->verify($payment_id,'Sent');
		redirect('admin/history/'.$payment_id);
	}

	public function get_details($cart_id){

		$this->load->model('product_model','prod');
		$this->load->model('cart_model','cart');
		$this->load->model('user_model','user');


		$cart = $this->cart->get_cart_row($cart_id);
		$user = $this->user->get_userdata($cart->user_id);

		$table = "";
		$table.= "
					<div style='font-weight:bold' id='tran_head'>
						<table>
							<tr height='30'>
								<td width='200'>Transaction ID</td>
								<td>$cart->cart_id</td>
							</tr>
							
							<tr height='30'>
								<td colspan='2'>
									<table style='border:1px solid #000'>
										<tr height='30'>
											<td width='200' colspan='2' style='text-align:center'>Customer Information</td>
										</tr>
										<tr>
											<td width='200'>Customer ID</td>
											<td>$user->user_id</td>
										</tr>
										<tr>
											<td width='200'>Name</td>
											<td>$user->username</td>
										</tr>
										<tr>
											<td width='200'>Address</td>
											<td>$user->address</td>
										</tr>
										<tr height='30'>
											<td width='200'>City</td>
											<td>$user->city</td>
										</tr>
										<tr height='30'>
											<td width='200'>Phone</td>
											<td>$user->phone</td>
										</tr>
										<tr height='30'>
											<td width='200'>Email</td>
											<td>$user->email</td>
										</tr>
									</table>
								</td>
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

	function encrypt_password_and_insert_callback($post_array) {
	  $post_array['password'] = md5($post_array['password']);

	  return $this->db->insert('user',$post_array);
	}

	public function changePassword(){
		$userid = $this->session->userdata('userid');
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('flexigrid');
			$crud->set_table('user');
			$crud->where('user_id',$userid);
			$crud->set_subject('Password');
			$crud->required_fields('old_password','new_password','confirm_new_password');
			$crud->fields('old_password','new_password','confirm_new_password');
			$crud->change_field_type('old_password', 'password');
			$crud->change_field_type('new_password', 'password');
			$crud->change_field_type('confirm_new_password', 'password');
			$crud->callback_update(array($this,'change'));
			$crud->unset_back_to_list();
			$crud->display_as('old_password','Old Password');
			$crud->set_rules('old_password', 'Verify Old Password', 'callback_password_check');
			$crud->set_rules('new_password', 'Verify Password', 'required|matches[confirm_new_password]');
    		$output = $crud->render();
			$data = array(
				"page_title"=>"Change Password",
				"main_menu"=>"mainmenu",
				"sidebar"=>"sidebar",
				"page_name"=>"crud",
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}

	public function encrypt_password($post_array) {
		$post_array['old_password'] = md5($post_array['old_password']);
		$post_array['new_password'] = md5($post_array['new_password']);
		$post_array['confirm_new_password'] = md5($post_array['confirm_new_password']);
	  	return $post_array;
	}

	public function change($post_array,$primary_key){
		$data = array(
			'password'=>md5($post_array['new_password'])
		);
		$this->db->update('user',$data,array('user_id' => $primary_key));
		return true;
	}

	public function password_check($str){
		$this->load->model("user_model", "mod");
		$id = $this->session->userdata('userid');
		$result = $this->mod->cekLogin($id, md5($str));
		if($result == 0){
			$this->form_validation->set_message('password_check', 'Old Password is wrong');
			return false;
		}
		return true;
	}
}