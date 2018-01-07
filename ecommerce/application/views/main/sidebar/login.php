<div class="sidebar-box">
    <h1 class="sidebar-head round5-top">Login</h1>
    <div class="sidebar-content">
        <div id="sidebar-login">
                <div class="label">
                    Username
                </div>
                <div class="clear5"></div>
                <div>
                    <input type="text" name="username" placeholder="Username" class="uniform" style="width:200px;" id="login_username" />
                    <div class="clear"></div>
                </div>
                <div class="clear5"></div>
                <div class="label">
                    Password
                </div>
                <div class="clear5"></div>
                <div>
                    <input type="password" name="password" placeholder="Password" class="uniform" style="width:200px;" id="login_password" />
                    <div class="clear"></div>
                </div> 
                <div class="clear10"></div>
                <div align="center" style="margin-bottom:5px;color:#F00" id="err_login"></div>
                <div style="text-align:right;">
                 	<a href="<?php echo base_url(); ?>main/register" style="color:#000"><button type="submit" class="uniform">Register</button></a>
                    <button type="button" class="uniform" id="login">Login</button>
                </div>
            <div class="clear10"></div>
        </div>
    </div>
</div>