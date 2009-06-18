using System;
using System.Collections.Generic;
using Ra.Widgets;
using Ra.Extensions;
using Doxygen.NET;
using System.Linq;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        foreach (Class c in Docs.GetNamespaceByName("Ra.Behaviors").Classes)
        {
            LinkButton classLink = new LinkButton();
            classLink.Text = c.FullName;
            classLink.Xtra = c.ID;
            classLink.Click += classLink_Click;
            typesPanel.Controls.Add(classLink);
            typesPanel.Controls.Add(new System.Web.UI.LiteralControl("<br />"));
        }
    }

    protected void classLink_Click(object sender, EventArgs e)
    {
        Namespace raWidgets = Docs.GetNamespaceByName("Ra.Behaviors");
       
       SelectedClass = (from c2 in raWidgets.Classes 
                        where c2.ID == (sender as LinkButton).Xtra 
                        select c2).Single() as Class;

       descriptionLabel.Text = SelectedClass.Description;
       members.LoadControls();
    }

    protected void members_Reload(object sender, Dynamic.ReloadEventArgs e)
    {
       foreach (Member item in SelectedClass.Members)
       {
           LinkButton memberLink = new LinkButton();
           memberLink.Text = item.Name;
           memberLink.Xtra = item.ID;
           memberLink.Click += memberLink_Click;
           members.Controls.Add(memberLink);
           members.Controls.Add(new System.Web.UI.LiteralControl("<br />"));
       }
    }

    protected void memberLink_Click(object sender, EventArgs e)
    {
        Member member = SelectedClass.Members.Find(delegate(Member m)
        {
            return m.ID == (sender as LinkButton).Xtra;
        });

        string methodSignature = string.Empty;
        if (member is Method)
        {
		    methodSignature = (member as Method).Signature;
        }

        parameters.Controls.Clear();
        parameters.Controls.Add(new System.Web.UI.LiteralControl(
            string.Format("<p>Access: {0}</p><p>Kind: {1}</p><p>{3}</p><p>Return Type: {4}</p><p>Description: {2}</p>", 
            member.AccessModifier, member.Kind, member.Description, methodSignature, member.ReturnType)));
        parameters.ReRender();
    }

    public Docs Docs 
    {
        get 
        {
            if (Application["docs"] == null)
                Application["docs"] = new Docs(Server.MapPath("~/docs-xml"));
            return (Docs)Application["docs"];
        }
        set 
        {
            Application["docs"] = value;
        }
    }

    public Class SelectedClass
    {
        get
        {
            if (Application["selected"] != null)
                return (Class)Application["selected"];
            return null;
        }
        set
        {
            Application["selected"] = value;
        }
    }
}
