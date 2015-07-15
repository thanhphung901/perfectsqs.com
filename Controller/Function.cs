using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vpro.functions;

namespace Controller
{
    public class Function
    {
        public string Getlink_News(object News_url, object Seo_url, object Cat_seo_url)
        {
            string _sType = Utils.CStrDef(Cat_seo_url);
            return string.IsNullOrEmpty(Utils.CStrDef(News_url)) ? "/" + _sType + "/" + Utils.CStrDef(Seo_url) + ".html" : Utils.CStrDef(News_url);
        }
        public string Getlink_Cat(object Cat_Url, object Cat_Seo_Url)
        {
            return string.IsNullOrEmpty(Utils.CStrDef(Cat_Url)) ? "/" + Utils.CStrDef(Cat_Seo_Url) + ".html" : Utils.CStrDef(Cat_Url);
        }
        public string Getprice(object Price)
        {
            decimal _dPrice = Utils.CDecDef(Price);
            return _dPrice != 0 ? String.Format("{0:0,0 VNĐ}", _dPrice) : "Contact";
        }
        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:MMM d, yyyy}", News_PublishDate);
        }
        public string GetImageT_News(object News_Id, object News_Image1)
        {

            try
            {
                if (Utils.CIntDef(News_Id) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(News_Image1)))
                {
                    return "<img src='" + PathFiles.GetPathNews(Utils.CIntDef(News_Id)) + Utils.CStrDef(News_Image1) + "' class='fullsize trans' />";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        public string GetImageT_News1(object News_Id, object News_Image1)
        {

            try
            {
                if (Utils.CIntDef(News_Id) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(News_Image1)))
                {
                    return PathFiles.GetPathNews(Utils.CIntDef(News_Id)) + Utils.CStrDef(News_Image1) ;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        public string GetImageT_News_Hasclass(object News_Id, object News_Image1, string nameclass)
        {

            try
            {
                if (Utils.CIntDef(News_Id) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(News_Image1)))
                {
                    return "<img class='" + nameclass + "' alt='' src='" + PathFiles.GetPathNews(Utils.CIntDef(News_Id)) + Utils.CStrDef(News_Image1) + "'/>";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        public string Getimages_Cat(object cat_id, object cat_img)
        {
            if (Utils.CIntDef(cat_id) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(cat_img)))
            {
                return PathFiles.GetPathCategory(Utils.CIntDef(cat_id)) + Utils.CStrDef(cat_img);
            }
            else
            {
                return "";
            }
        }
        public string GetImageAd(object Ad_Id, object Ad_Image1, object Ad_Target, object Ad_Url)
        {
            try
            {
                if (Utils.CIntDef(Ad_Id) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(Ad_Image1)))
                    return "<a href='" + Utils.CStrDef(Ad_Url) + "' target='" + Utils.CStrDef(Ad_Target) + "'><img src='" + PathFiles.GetPathAdItems(Utils.CIntDef(Ad_Id)) + Utils.CStrDef(Ad_Image1) + "' alt='' /></a>";
                return "";
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }

        }
        public string GetImageAds1(object Ad_Id, object Ad_Image1, object Ad_Target, object Ad_Url)
        {
            try
            {
                if (Utils.CIntDef(Ad_Id) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(Ad_Image1)))
                    return "<a href='" + Utils.CStrDef(Ad_Url) + "' target='" + Utils.CStrDef(Ad_Target) + "' class='ads_item'><img  width='100%' src='" + PathFiles.GetPathAdItems(Utils.CIntDef(Ad_Id)) + Utils.CStrDef(Ad_Image1) + "' alt='' /></a>";
                return "";
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }

        }
        public string GetImagePartner(object Ad_Id, object Ad_Image1, object Ad_Target, object Ad_Url, object Ad_Des)
        {
            try
            {
                if (Utils.CIntDef(Ad_Id) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(Ad_Image1)))
                    return "<a href='" + Utils.CStrDef(Ad_Url) + "' target='" + Utils.CStrDef(Ad_Target) + "' class='thumb'><img  width='100%' src='" + PathFiles.GetPathAdItems(Utils.CIntDef(Ad_Id)) + Utils.CStrDef(Ad_Image1) + "' alt='' /><div class='title' title=''>" + Utils.CStrDef(Ad_Des) + "</div></a>";
                return "";
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }

        }
        //Get images logo - sologan
        public string Getlogo(object Banner_type, object banner_field, object Banner_ID, object Banner_Image)
        {
            return "<a href='/'>" + GetImage(Banner_type, Banner_ID, Banner_Image) + "</a>";
        }
        public string Getbanner(object Banner_type, object banner_field, object Banner_ID, object Banner_Image)
        {
            return "<p id='banner_head' class='nine columns'>" + GetImagebanner(Banner_type, Banner_ID, Banner_Image) + "</p>";
        }
        public string GetImage(object Banner_type, object Banner_ID, object Banner_Image)
        {
            try
            {
                string _sResult = string.Empty;
                if (Utils.CIntDef(Banner_type) == 0)
                {
                    if (Utils.CIntDef(Banner_ID) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(Banner_Image)))
                        return "<img src='" + PathFiles.GetPathBanner(Utils.CIntDef(Banner_ID)) + Utils.CStrDef(Banner_Image) + "' alt=''/>";
                    else
                        return "<img src='/vi-vn/Images/Logo.png'/>"; ;
                }
                else
                {
                    if (Utils.CIntDef(Banner_ID) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(Banner_Image)))
                    {
                        _sResult += "<object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0'  width='270' height='83' id='ShockwaveFlash1' >";
                        _sResult += "<param name='movie' value='" + PathFiles.GetPathAdItems(Utils.CIntDef(Banner_ID)) + Utils.CStrDef(Banner_Image) + "'>";
                        _sResult += "<param name='Menu' value='0'>";
                        _sResult += "<param name='quality' value='high'>";
                        _sResult += "<param name='wmode' value='transparent'>";
                        _sResult += "<embed width='270' height='83' width='100%' src='" + PathFiles.GetPathBanner(Utils.CIntDef(Banner_ID)) + Utils.CStrDef(Banner_Image) + "' wmode='transparent' ></object>";
                    }

                }
                return _sResult;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        public string GetImagebanner(object Banner_type, object Banner_ID, object Banner_Image)
        {
            try
            {
                string _sResult = string.Empty;
                if (Utils.CIntDef(Banner_type) == 0)
                {
                    if (Utils.CIntDef(Banner_ID) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(Banner_Image)))
                        return "<img src='" + PathFiles.GetPathBanner(Utils.CIntDef(Banner_ID)) + Utils.CStrDef(Banner_Image) + "' alt='' />";
                    else
                        return "<img src='/vi-vn/Images/Logo.png'/>"; ;
                }
                else
                {
                    if (Utils.CIntDef(Banner_ID) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(Banner_Image)))
                    {
                        _sResult += "<object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0'  width='865' height='60' id='ShockwaveFlash1' >";
                        _sResult += "<param name='movie' value='" + PathFiles.GetPathAdItems(Utils.CIntDef(Banner_ID)) + Utils.CStrDef(Banner_Image) + "'>";
                        _sResult += "<param name='Menu' value='0'>";
                        _sResult += "<param name='quality' value='high'>";
                        _sResult += "<param name='wmode' value='transparent'>";
                        _sResult += "<embed width='865' height='60' width='100%' src='" + PathFiles.GetPathBanner(Utils.CIntDef(Banner_ID)) + Utils.CStrDef(Banner_Image) + "' wmode='transparent' ></object>";
                    }

                }
                return _sResult;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        //Support
        public string Bind_Online(object Type, object Description, object Nickname, object Field1, object Field2)
        {

            try
            {
                //{0}{1}yahoo - nickname
                //{2}{3}skype - Field 1
                //{4}Description
                //{5}Hotline - Field2
                int _iType = Utils.CIntDef(Type);
                string _sResult = string.Empty;
                if (_iType == 0)
                {
                    string str = "";
                    if (Utils.CStrDef(Nickname) != "")
                    {
                        str += String.Format(@"<div><span class='tt_support'>{0}</span><a title='' href='ymsgr:sendim?{1}' class='iframe topopup yahoo' data-fancybox-type='iframe'><img  alt='' src='../vi-vn/Images/yahoo-online-icon.png' /></a></div>"
                            , Utils.CStrDef(Description), Utils.CStrDef(Nickname), Utils.CStrDef(Nickname));
                    }
                    if (Utils.CStrDef(Field1) != "")
                    {
                        str += String.Format(@"<div><span class='tt_support'>{0}</span><a title='' href='skype:{1}?chat' class='iframe topopup sky' data-fancybox-type='iframe'><img alt='' src='../vi-vn/Images/skype_icon2.png'></a></div>"
                            , Utils.CStrDef(Description), Utils.CStrDef(Field1), Utils.CStrDef(Field1));
                    }
                    //if (Utils.CStrDef(Field2) != "")
                    //{
                    //    str += String.Format(@"<br /><span class='title_sp'>{0}</span> <span class='number_phone'>{1}</span>"
                    //        , Utils.CStrDef(Description), Utils.CStrDef(Field2));
                    //}
                    //_sResult = String.Format(str, Utils.CStrDef(Nickname), Utils.CStrDef(Nickname), Utils.CStrDef(Field1), Utils.CStrDef(Field1), Utils.CStrDef(Description), Utils.CStrDef(Field2));
                    _sResult = str;
                }
                return _sResult;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
    }
}
