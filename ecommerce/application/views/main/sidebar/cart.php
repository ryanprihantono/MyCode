<div class="sidebar-box">
    <h1 class="sidebar-head round5-top">My Cart</h1>
    <div class="sidebar-content">
        <div class="sidebar-cart">
            <a href="<?php echo site_url("main/mycart"); ?>">
                <img width="150" src="<?php echo base_url(); ?>assets/main/images/icon/shopping_cart.png">
            </a>
        </div>
        <!--<p class="sidebar-cart-text">Now <strong>0 item(s)</strong> in your cart</p>-->
        <div class="sidebar-cart-text" id="cart_item_wrap">
        	<ul>
            	<li></li>        	
				<?php echo $items_cart; //$this->session->sess_destroy();?>
                
            </ul>
        </div>
    </div>
</div>