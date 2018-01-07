<h1 class="head-mainbar round5"><?php echo $item->item; ?></h1>
<div id="detail-product">
    <div id="detail-product-left">
        <div class="highslide-gallery">
        	
            <?php
				foreach($pictures->result() as $row){
			?>
            <div style="z-index:auto">
            <a href="<?php echo base_url(); ?>assets/uploads/images/<?php echo $row->picture; ?>" class="jqzoom" title="<?php echo "|".$row->title."|"; ?>">
                <img src="<?php echo base_url(); ?>assets/uploads/images/<?php echo $row->picture; ?>" width="130" height="130"/>
            </a> 
            </div>
            <div class="highslide-heading">
                <?php echo $row->picture; ?>
            </div>
            <?php
				}
			?>
            
        </div>
    </div>
    <div id="detail-product-right">
        <p class="detail-product-label">
            Description:
        </p>
        <p class="detail-product-desc">
            <?php echo $item->description; ?>
        </p>
        <div id="detail-product-action">
            
                <div class="price" style="margin-top:10px;font-size:20px">
                        Rp. <?php echo $item->price; ?>
                    </div>

                    <div class="size" style="float:left;margin:20px 10px 0 0;">
                    	<select id="size_<?php echo $item->item_id; ?>_false"><option value="0">-Size-</option></select>
                    </div>
                    <div class="color" style="margin:20px 0 0 0">
                        <select id="color_<?php echo $item->item_id; ?>_false"><option value="0">-Color</option></select>
                    </div>
                  <div class="clear10" style="width:80px;float:left;margin-top:10px"><label>Qty</label>
                      <input name="quantity" type="text" class="uniform" size="3" value="1" id="quantity_<?php echo $item->item_id; ?>"/></div>
                <div class="clear5"></div>
            <div class="action">
                <input type="image" src="<?php echo base_url(); ?>assets/main/images/icon/add-to-cart.png" id="<?php echo base_url()."_$item->item_id"; ?>" class="btn-addtocart" title="Add To Cart" />
            </div>
            <div class="clear5"></div>
        </div>
    </div>
</div>