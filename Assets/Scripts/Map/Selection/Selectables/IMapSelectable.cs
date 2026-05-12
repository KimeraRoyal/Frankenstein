namespace Bodybuilder.Map.Selection.Selectables
{
    public interface IMapSelectable
    {
        public void Select();
        
        public IMapSelectable GetLayer();
    }
}