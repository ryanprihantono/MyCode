<h1 class="head-mainbar margin-top-20 round5">Featured Product</h1>
<?php if(sizeof($product)>0){ ?>
<div class="product">
    <?php
        $num_items = sizeof($featured_product);
		$num_pages = 1;
        $items_perpages = 3;
        $last_pages = $num_items;
		if($num_items>$items_perpages){
			$num_pages = round($num_items/3);
			if($num_items%$items_perpages!=0){
				$num_pages++;
				$last_pages = $num_items%$items_perpages;
			}
		}

        $item_idx=0;
        for($i=0;$i<$num_pages;$i++){
            if($last_pages>0 && $i==$num_pages-1){
                $items_perpages = $last_pages;
            }
    ?>
        <div class="slide">
    <?php for($j=0;$j<$items_perpages;$j++){
            //echo $j;
            $row = $featured_product[$item_idx];
            $item_idx++;
    ?>
            <div class="product-box">
                <div class="productname"><a href="<?php echo base_url()."main/detail_product/".$row->item_id; ?>"><?php echo $row->item; ?></a></div>
                <div class="product-content">
                	<!--<div class="point">
                        	 <?php //echo $row->point; ?><br />
                    </div>-->
                    <div class="img-box">
                        <a href="<?php echo base_url()."main/detail_product/".$row->item_id; ?>">
                            <img width="194" src="<?php echo base_url(); ?>assets/uploads/images/<?php echo $row->picture; ?>">
                        </a>
                    </div>
                    <div class="price">
                        Rp. <?php echo $row->price; ?>
                    </div>

                    <div class="color" style="float:left;margin:20px 0 20px 0">
                        <select id="color_<?php echo $row->item_id; ?>_false"><option value="0">-Color-</option></select>
                    </div>
                  	<div style="margin-top:20px;margin-left:10px;float:right">
                    	<label>Qty</label>
                      	<input name="quantity" type="text" class="uniform" size="3" value="1" id="quantity_<?php echo $row->item_id; ?>"/>
                    </div>
                    <div class="action">

                        <input type="image" src="<?php echo base_url(); ?>assets/main/images/icon/add-to-cart.png" id="<?php echo base_url()."_".$row->item_id; ?>" class="btn-addtocart" title="Add To Cart" />
                  </div>
                    <div class="clear"></div>
                </div>
            </div>
    <?php

            } ?>
        </div>
    <?php
        }
    ?>

</div>
<?php if($num_pages>1){ ?>
<div class="nav">
	<a href="#" class="prev"><img src="<?php echo base_url(); ?>assets/main/images/js/img/arrow-prev.png" width="24" height="43" alt="Arrow Prev"></a>
	<a href="#" class="next"><img src="<?php echo base_url(); ?>assets/main/images/js/img/arrow-next.png" width="24" height="43" alt="Arrow Next"></a>
</div>
<?php } ?>
<?php } ?>
<div class="clear"></div>