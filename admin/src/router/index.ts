import { Router } from "@vaadin/router";

const importPage = () => {
  const router = new Router(document.getElementById("app"));
  router.setRoutes([
    {
      path: "/",
      component: "app-home",
      animate: true,
      // @ts-ignore
      action: () => import("../views/app-home")
    },
    {
      path: "/list",
      animate: true,
      component: "app-list",
      // @ts-ignore
      action: () => import("../views/app-list")
    },
    {
      path: "/app/:id",
      component: "app-detail",
      animate: true,
      // @ts-ignore
      action: () => import("../views/app-detail")
    }

  ]);
};
window.addEventListener("load", () => {
    console.log('loaded')
  importPage();
});