
<h1 class="head-mainbar round5">Register </h1>
    <form class="uniform" style="padding-left: 30px;" action="" method="post" enctype="multipart/form-data">
        
        <div>
            <label class="uniform">Username<font color="#FF0000">*</font></label>
            <input type="text" name="username" id="username" placeholder="Username" class="uniform" style="width:200px;" />
            <div id="err_username"></div>
            <div class="clear"></div>
        </div>
        <div>
            <label class="uniform">Email<font color="#FF0000">*</font></label>
            <input type="text" name="email" id="email" placeholder="Email" class="uniform" style="width:200px;" />
            <div id="err_email"></div>
            <div class="clear"></div>
        </div>
        <div>
            <label class="uniform">Password<font color="#FF0000">*</font></label>
            <input type="password" name="password" id="password" placeholder="Password" class="uniform" />
            <div id="err_password"></div>
            <div class="clear"></div>
        </div>
        <div>
            <label class="uniform">Re-Password<font color="#FF0000">*</font></label>
            <input type="password" name="conpassword" id="conpassword" placeholder="Confirm Password" class="uniform" />
            <div id="err_conpassword"></div>
            <div class="clear"></div>
        </div>
         <div style="height:auto;margin-bottom:-60px">
            <label class="uniform">Address<font color="#FF0000">*</font></label>
            <textarea name="address" id="address" placeholder="Address" class="uniform"></textarea>
            <div id="err_address"></div>
            <div class="clear"></div>
        </div>
        <div style="margin-top:auto">
            <label class="uniform">City<font color="#FF0000">*</font></label>
            <input type="text" name="city" id="city" placeholder="City" class="uniform" />
            <div id="err_city"></div>
            <div class="clear"></div>
        </div>
        <div style="margin-top:auto">
            <label class="uniform">Phone<font color="#FF0000">*</font></label>
            <input type="text" name="phone" id="phone" placeholder="Phone" class="uniform" />
            <div id="err_phone"></div>
            <div class="clear"></div>
        </div>
        <div>
            <label class="uniform">I am</label>
            <select name="gender" class="uniform">
                <option value="Male">Male</option>
                <option value="Female">Female</option>
            </select>
            <div class="clear"></div>
        </div>
        <div>
            <label class="uniform">Birthday</label>
            <input type="text" name="birthday" id="birthday" placeholder="Birthday" class="uniform datepicker" />
            <div id="err_birthday"></div>
        </div>
        <div>
            <button type="button" class="uniform" id="register">Register</button>
        </div>
    </form>
<div class="clear"></div>