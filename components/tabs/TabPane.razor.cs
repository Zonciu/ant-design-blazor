using Microsoft.AspNetCore.Components;

namespace AntDesign
{
    public partial class TabPane : AntDomComponentBase
    {
        private const string PrefixCls = "ant-tabs-tab";

        internal bool IsActive { get; set; }
        internal bool HasRendered { get; set; }

        internal ElementReference TabRef { get; set; }

        public TabPane()
        {
        }

        public TabPane(string key, RenderFragment tab, RenderFragment childContent)
        {
            this.Key = key;
            this.Tab = tab;
            this.ChildContent = childContent;
        }

        [CascadingParameter]
        internal Tabs Tabs { get; set; }

        [CascadingParameter(Name = "IsTab")]
        internal bool IsTab { get; set; }

        [CascadingParameter(Name = "IsContent")]
        internal bool IsContent { get; set; }

        /// <summary>
        /// Forced render of content in tabs, not lazy render after clicking on tabs
        /// </summary>
        [Parameter]
        public bool ForceRender { get; set; } = false;

        /// <summary>
        /// TabPane's key
        /// </summary>
        [Parameter]
        public string Key { get; set; }

        /// <summary>
        /// Show text in <see cref="TabPane"/>'s head
        /// </summary>
        [Parameter]
        public RenderFragment Tab { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Closable { get; set; } = true;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            ClassMapper.Clear().
              Add(PrefixCls)
              .If($"{PrefixCls}-active", () => IsActive)
              .If($"{PrefixCls}-with-remove", () => Closable)
              .If($"{PrefixCls}-disabled", () => Disabled);

            Tabs?.AddTabPane(this);
        }

        protected override void Dispose(bool disposing)
        {
            Tabs?._panes.Remove(this);
            base.Dispose(disposing);
        }
    }
}
