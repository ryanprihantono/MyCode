<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class Cart_model extends CI_Model {

	public function __construct(){
		parent::__construct();
	}
	public function get_last_cart($user_id){
		$query = $this->db->query("select * from cart where user_id=$user_id order by cart_id desc");
		return $query->first_row();
	}
	public function get_cart_row($cart_id,$user_id=NULL){
		$string = "select * from cart where cart_id=$cart_id and user_id=$user_id";
		if($user_id==NULL){
			$string = "select * from cart where cart_id=$cart_id";
		}
		$query = $this->db->query($string);
		return $query->first_row();
	}
	public function get_carts($user_id){
		$query = $this->db->query("select * from cart where user_id=$user_id order by cart_id desc");
		$carts = array();
		$cart = array();
		$i = 0;
		if($query->num_rows()>0){
			foreach($query->result() as $row){
				$cart['cart_id'] 	= $row->cart_id;
				$cart['date']		= $row->date;
				$cart['bill_code'] 	= $row->bill_code;
				$cart['total_bill']	= $row->total_bill;
				$cart['status']		= $this->payment_status($row->cart_id);
				$carts[$i] = $cart;
				$i++;
			}
		}
		return $carts;
	}
	public function payment_status($cart_id){
		$query = $this->db->query("select * from payment where cart_id=$cart_id order by payment_id desc");
		if($query->num_rows()>0){
			return $query->first_row()->payment_status;
		}
		return "Unpaid";
	}
	public function get_bank(){
		$query = $this->db->query("select * from bank");
		return $query;
	}
	public function dopayment($cart_id,$bank_id,$payment_account,$amount_paid,$date,$account_name){
		$query = $this->db->query("insert into payment (cart_id,date,amount_paid,bank_id,payment_account,account_name) values($cart_id,'$date',$amount_paid,$bank_id,'$payment_account','$account_name')");
		return $query;
	}
	public function verify($payment_id,$payment_status){
		$query = $this->db->query("update payment set payment_status='$payment_status' where payment_id=$payment_id");
		if($payment_status=='Verified'){
			$this->update_stock($payment_id);
		}
		return $query;
	}
	public function get_cart_id($payment_id){
		$query = $this->db->query("select * from payment where payment_id=$payment_id");
		return $query->first_row()->cart_id;
	}
	public function get_cart_detail($cart_id){
		$query = $this->db->query("select * from cart_detail where cart_id=$cart_id");
		return $query;
	}
	public function update_stock($payment_id){
		$query = $this->db->query("select * from payment where payment_id=$payment_id");
		$cart_id = $query->first_row()->cart_id;
		$query = $this->db->query("select * from cart_detail where cart_id=$cart_id");
		foreach($query->result() as $row){
			$this->update_qty($row->item_id,$row->qty);
			//$this->update_size($row->item_id,$row->size_id,$row->qty);
			$this->update_color($row->item_id,$row->color_id,$row->qty);
		}
	}
	public function update_qty($item_id,$qty){
		$query = $this->db->query("select * from item where item_id=$item_id");
		$stock = $query->first_row()->total_stock;
		$stock -= $qty;
		$this->db->query("update item set total_stock=$stock where item_id=$item_id");
	}
	public function update_size($item_id,$size_id,$qty){
		$query = $this->db->query("select * from tr_size where size_id=$size_id and item_id=$item_id");
		$stock = $query->first_row()->stock;
		$stock -= $qty;
		$this->db->query("update tr_size set stock=$stock where size_id=$size_id and item_id=$item_id");
	}
	public function update_color($item_id,$color_id,$qty){
		$query = $this->db->query("select * from tr_color where color_id=$color_id and item_id=$item_id");
		$stock = $query->first_row()->stock;
		$stock -= $qty;
		$this->db->query("update tr_color set stock=$stock where color_id=$color_id and item_id=$item_id");
	}
}