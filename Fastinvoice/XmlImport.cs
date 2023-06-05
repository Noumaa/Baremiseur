using Baremiseur.Contexts;
using Baremiseur.Models;
using System.Windows;
using System.Xml;

namespace Baremiseur
{
    internal class XmlImport
    {
        public void Import(string filePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            /*string _byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            if (xmlDoc.StartsWith(_byteOrderMarkUtf8))
            {
                xmlDoc = xmlDoc.Remove(0, _byteOrderMarkUtf8.Length);
            }*/

            XmlNode root = xmlDoc.DocumentElement;

            ProcessNode(root, null);
        }

        private void ProcessNode(XmlNode node, Skill parentSkill)
        {
            //MessageBox.Show(" process ");

            string debug = "";
            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name == "value") continue;

                string name = child.SelectSingleNode("value").InnerText;
                //MessageBox.Show("name " + name);

                Skill skill = new Skill { Name = name, ParentSkillId = parentSkill?.Id };

                using (var db = new StudentsContext())
                {
                    db.Skills.Add(skill);
                    db.SaveChanges();
                }

                if (node.Attributes.Count != 0) debug += node.Attributes?["id"].Value + "  " + node.Attributes?["type"].Value + "\n";
                ProcessNode(child, skill);
            }
        }
    }
}
