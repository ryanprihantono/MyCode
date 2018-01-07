<h1 class="head-mainbar round5">New Arrival </h1>
<!--<?php /*if(sizeof($product)>0){ ?>
<div class="product">
    <?php

        $num_items = sizeof($product);
		$num_pages = 1;
        $items_perpages = 4;
        $last_pages = 0;
		if($num_items>$items_perpages){
			$num_pages = floor($num_items/$items_perpages);

			if($num_items%$items_perpages!=0){
				$num_pages+=1;
				$last_pages = $num_items%$items_perpages;
			}
		}

        $item_idx=0;
        for($i=0;$i<$num_pages;$i++){
            if($last_pages>0){
				if($i == $num_pages-1){
                	$items_perpages = $last_pages;
				}
            }
    ?>
        <div class="slide">
    <?php for($j=0;$j<$items_perpages;$j++){
            echo $j;
            $row = $product[$item_idx];
            $item_idx++;
			$desc = $row->description;
    ?>
            <div class="product-box round5">
                <div class="productname"><a href="<?php echo base_url()."main/detail_product/".$row->item_id; ?>"><?php echo $row->item; ?></a></div>
                <div class="product-content">
                    <div class="img-box">
                        <a href="<?php echo base_url()."main/detail_product/".$row->item_id; ?>" class="desc"  name="Description :<br><br><?php echo $desc; ?>">
                            <img width="150" src="<?php echo base_url(); ?>assets/uploads/images/<?php echo $row->picture; ?>">
                        </a>
                    </div>
                    <div class="price">
                        Rp. <?php echo $row->price; ?>
                    </div>

                    <div class="color" style="float:left;margin:10px 0 10px 0">
                        <select id="color_<?php echo $row->item_id; ?>_false"><option value="0">-Color-</option></select>
                    </div>
                  	<div style="margin-top:10px;margin-left:5px;float:right">
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
            } 
	?>
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
<?php } */?>-->
<div class="clear"></div>
<div align="center" style="margin:30px 0 50px 0">
<ul id="mycarousel" class="jcarousel-skin-tango">
	<?php foreach($product as $row){ $desc = $row->description; ?>
    
    <li><a href="<?php echo base_url()."main/detail_product/".$row->item_id; ?>" class="desc"  name="Description :<br><br><?php echo $row->description;; ?>"><img src="<?php echo base_url(); ?>assets/uploads/images/<?php echo $row->picture; ?>" width="75" alt="" /></a></li>
    
    <?php } ?>
</ul>
</div>
<div class="clear"></div>
<h1 class="head-mainbar round5">Welcome to Kent Fashion House</h1>
<div class="article"><?php echo $home_content; ?></div>
<div class="clear"></div>