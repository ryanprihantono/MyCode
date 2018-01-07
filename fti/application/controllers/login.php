<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class Login extends CI_Controller {

	public function __construct(){
		parent::__construct();
	}
	
	public function output($output = null){
		$this->load->view("default",$output);	
	}
	
	public function index(){
		$data = array(
			"web_title"=>"Sistem Inventory FTI UKSW"
		);
		$this->load->view('login',$data);
	}
	
	public function doLogin(){
		$this->load->model("usermodel", "mod");
		$username = $this->input->post("txtusername");
		$password = md5($this->input->post("txtpassword"));
		$result = $this->mod->cekLogin($username, $password);
		if($result == 1){
			$user = $this->mod->user($username);
			$this->session->set_userdata("login",true);
			$this->session->set_userdata($user);
			$pages = $this->mod->get_pages($this->session->userdata('id_role'));
			
			$this->session->set_userdata("pages",$pages);
			$role = $this->mod->get_role($this->session->userdata('id_role'));
			$this->session->set_userdata('role',$role);
			redirect("application");
		}else{
			redirect("login?err=Username and Password is not match");
		}
	}
	
	public function changePassword(){
		$id = $this->session->userdata('id');
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('flexigrid');
			$crud->set_table('users');
			$crud->where('id',$id);
			$crud->set_subject('Users');
			$crud->required_fields('password','new_password','confirm_new_password');
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
				"web_title"=>"Process",
				"page_title"=>"Add Process",
				"navigasi"=>"menu_utama",
				"toolbar"=>"none",
				"page_name"=>"crud",
				"sidebar"=>"sidebar_utama",
				"dashboard"=>"active",
				"report"=>""
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
		$this->db->update('users',$data,array('id' => $primary_key));
		return true;
	}
	
	public function password_check($str){
		$this->load->model("usermodel", "mod");
		$username = $this->session->userdata('username');
		$result = $this->mod->cekLogin($username, md5($str));
		if($result == 0){
			$this->form_validation->set_message('password_check', 'Old Password is wrong');
			return false;
		}
		return true;
	}
	
	public function doLogout(){
		$this->session->sess_destroy();
		redirect('login');
	}
}