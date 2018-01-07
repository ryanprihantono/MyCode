<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class Login_model extends CI_Model {

	public function __construct(){
		parent::__construct();
	}
	
	public function cekLogin($user_id, $password){
		$query = $this->db->query("SELECT * FROM user WHERE user_id = '$user_id' AND password = '$password'");
		if($query->num_rows() > 0)
			return 1;
		return 0;
	}
	
	public function statusLogin($user_id){
		$query = $this->db->query("SELECT * FROM user WHERE user_id = '$user_id'");
		$get = $query->first_row();
		return $get;
	}
}