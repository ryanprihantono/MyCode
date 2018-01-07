<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class Usermodel extends CI_Model {

	public function __construct(){
		parent::__construct();
	}
	
	public function cekLogin($username, $password){
		$query = $this->db->query("SELECT * FROM users WHERE username = '$username' AND password = '$password'");
		if($query->num_rows() > 0)
			return 1;
		return 0;
	}
	public function user($username){
		$query = $this->db->query("SELECT * FROM users WHERE username = '$username'");
		$get = $query->first_row();
		return $get;
	}
	public function get_role($id_role){
		$query = $this->db->query("SELECT * FROM roles WHERE id_role = $id_role");
		$role;
		foreach($query->result() as $row){
			$role=$row->role;
		}
		return $role;
	}
	public function get_pages($id_role){
		$query = $this->db->query("select * from tr_page where id_role=$id_role");
		$pages = array();
		$i =1;
		foreach($query->result() as $row){
			$pages[$i] = $row->id_page;
			$i++;
		}
		return $pages;
	}
}