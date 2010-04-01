using System;
using Ra.Widgets;
using Doxygen.NET;
using System.Linq;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        foreach (Class c in Docs.GetNamespaceByName("Ra.Behaviors").Classes)
        {
            LinkButton classLink = new LinkButton { Text = c.FullName, Xtra = c.ID };
            classLink.Click += classLink_Click;
            typesPanel.Controls.Add(classLink);
            typesPanel.Controls.Add(new System.Web.UI.LiteralControl("<br />"));
        }
    }

    private void classLink_Click(object sender, EventArgs e)
    {
        Namespace raWidgets = Docs.GetNamespaceByName("Ra.Behaviors");
       
       SelectedClass = (from c2 in raWidgets.Classes 
                        where c2.ID == ((LinkButton) sender).Xtra 
                        select c2).Single() as Class;

       if (SelectedClass == null)
           return;
        
        descriptionLabel.Text = SelectedClass.Description;
        members.LoadControls();
    }

    protected void members_Reload(object sender, Dynamic.ReloadEventArgs e)
    {
       foreach (Member item in SelectedClass.Members)
       {
           LinkButton memberLink = new LinkButton { Text = item.Name, Xtra = item.ID };
           memberLink.Click += memberLink_Click;
           members.Controls.Add(memberLink);
           members.Controls.Add(new System.Web.UI.LiteralControl("<br />"));
       }
    }

    private void memberLink_Click(object sender, EventArgs e)
    {
        Member member = SelectedClass.Members.Find(m => m.ID == ((LinkButton) sender).Xtra);

        string methodSignature = string.Empty;

        if (member is Method)
		    methodSignature = (member as Method).Signature;

        parameters.Controls.Clear();
        parameters.Controls.Add(new System.Web.UI.LiteralControl(
            string.Format("<p>Access: {0}</p><p>Kind: {1}</p><p>{3}</p><p>Return Type: {4}</p><p>Description: {2}</p>", 
                member.AccessModifier, 
                member.Kind, 
                member.Description, 
                methodSignature, 
                member.ReturnType)));
        parameters.ReRender();
    }

    private Docs Docs 
    {
        get 
        {
            if (Application["Docs"] == null)
                Application["Docs"] = new Docs(Server.MapPath("~/docs-xml"));
            return (Docs)Application["Docs"];
        }
        set 
        {
            Application["Docs"] = value;
        }
    }

    private Class SelectedClass
    {
        get
        {
            if (ViewState["SelectedClass"] != null)
                return (Class)ViewState["SelectedClass"];
            return null;
        }
        set
        {
            ViewState["SelectedClass"] = value;
        }
    }
}
