using TreeStructuresDemo.Models;

namespace TreeStructuresDemo.Scenarios;

public static class DemoDatasetFactory
{
    public static DemoDataset CreateBaseDataset()
    {
        return new DemoDataset
        {
            Title = "Content Tree Structures Demo",
            Description = "Compare general trees, binary trees, BSTs, and AVL trees in a content catalog domain.",
            RootCategory = new ContentCategory(1, "Knowledge Base"),
            ContentItems =
            [
                new(10, "Getting Started"),
                new(20, "Billing FAQ"),
                new(30, "Troubleshooting"),
                new(40, "Security Guide"),
                new(50, "Release Notes"),
                new(60, "Admin Overview")
            ],
            SearchItems =
            [
                new(10, "Getting Started"),
                new(20, "Billing FAQ"),
                new(30, "Troubleshooting"),
                new(40, "Security Guide"),
                new(50, "Release Notes"),
                new(25, "Profile Settings")
            ]
        };
    }
}