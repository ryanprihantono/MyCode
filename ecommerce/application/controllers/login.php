<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class Login extends CI_Controller {
	
	public function __construct(){
		parent::__construct();
    }
	
	public function index(){
		if($this->session->userdata('username')!=NULL){
			redirect('admin');
		}
		$data = array(
			"web_title"=>"Login"
		);
		$this->load->view('admin/login',$data);
	}
	
	public function dologin(){
		$username = $this->input->post("username");
		$password = md5($this->input->post("password"));
		if($username==""||$password==""){
			redirect("login/?err=Username and Password must be filled");
		}
		$cek = $this->db->query("SELECT * FROM user WHERE username = '$username' AND password = '$password'");
		if($cek->num_rows() > 0){
			$get = $cek->row();
			$this->session->set_userdata("username",$get->username);
			$this->session->set_userdata("userid",$get->user_id);
			redirect("admin");
		}else{
			redirect("login/?err=Username and Password not match");
		}
	}
	
	public function dologout(){
		$this->session->sess_destroy();
		redirect("login");
	}
}